using System.ComponentModel;
using __Tests__.faker;
using bookmanager.exceptions;
using bookmanager.models;
using bookmanager.services;
using Moq;

namespace __Tests__;

public class BookManagerTest 
{
  private readonly Mock<IBookService> _mockBookService;
  private readonly Mock<IUserService> _mockUserService;
  private readonly Mock<ILogger> _mockLoggerService;
  private readonly BookManager _bookManager;

  private Faker<Book> _faker = new();
  public BookManagerTest ()
  {
    _mockBookService = new();
    _mockUserService = new();
    _mockLoggerService = new();
    _bookManager = new (_mockBookService.Object,_mockUserService.Object,_mockLoggerService.Object);
  }

  [Fact]
  [Description("BK-01 : Ajout d'un livre avec toutes les informations valides")]
  public void AddBookWithValidData()
  {
    //Arrange
    string key = "valid-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    //Act
    bool result = _bookManager.AddBook(sample);

    //Assert
    Assert.True(result);
    _mockBookService.Verify(s=>s.Add(sample),Times.Once);
  }

  [Fact]
  [Description("BK-02 : Ajout d'un livre avec un ISBN déjà existant")]
  public void AddDuplicateISBNkExpectException()
  {
    //Arrange
    string key = "valid-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    _mockBookService.Setup(s=>s.GetByISBN(sample.ISBN))
      .Returns(()=>throw new ISBNDuplicateException());

    //Act && Assert
    Assert.Throws<ISBNDuplicateException>(() => _bookManager.AddBook(sample));
    _mockBookService.Verify(s=>s.Add(sample),Times.Exactly(0));
    _mockBookService.Verify(s=>s.GetByISBN(sample.ISBN),Times.Exactly(1));
  }


  [Fact]
  [Description("BK-03 : Ajout d'un livre avec des champs obligatoires manquants")]
  public void AddBookMissingTitle()
  {
    //Arrange
    string key = "missing-title-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    //Act 
    EmptyFieldException exception = Assert.Throws<EmptyFieldException>(() => _bookManager.AddBook(sample));

    //Assert
    Assert.Contains("Title", exception.MissingFields);
    _mockBookService.Verify(s=>s.Add(sample),Times.Exactly(0));
    _mockBookService.Verify(s=>s.GetByISBN(sample.ISBN),Times.Exactly(0));
  }


  [Fact]
  [Description("BK-03 : Ajout d'un livre avec des champs obligatoires manquants")]
  public void AddBookMissingISBN()
  {
    //Arrange
    string key = "missing-ISBN-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    //Act 
    EmptyFieldException exception = Assert.Throws<EmptyFieldException>(() => _bookManager.AddBook(sample));

    //Assert
    Assert.Contains("ISBN", exception.MissingFields);
    _mockBookService.Verify(s=>s.Add(sample),Times.Exactly(0));
    _mockBookService.Verify(s=>s.GetByISBN(sample.ISBN),Times.Exactly(0));
  }

  [Fact]
  [Description("BK-03 : Ajout d'un livre avec des champs obligatoires manquants")]
  public void AddBookMissingTitleAndISBN()
  {
    //Arrange
    string key = "missing-title-ISBN-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    //Act 
    EmptyFieldException exception = Assert.Throws<EmptyFieldException>(() => _bookManager.AddBook(sample));

    //Assert
    Assert.Contains("ISBN", exception.MissingFields);
    Assert.Contains("Title", exception.MissingFields);
    _mockBookService.Verify(s=>s.Add(sample),Times.Exactly(0));
    _mockBookService.Verify(s=>s.GetByISBN(sample.ISBN),Times.Exactly(0));
  }

  [Fact]
  [Description("BK-04 : Ajout d'un livre avec un nombre de copies négatif")]
  public void AddBookInvalidCopies()
  {
    //Arrange
    string key = "invalid-copies-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    //Act 
    InvalidFieldException exception = Assert.Throws<InvalidFieldException>(() => _bookManager.AddBook(sample));

    //Assert
    Assert.Contains("Copies", exception.InvalidFields);
    _mockBookService.Verify(s=>s.Add(sample),Times.Exactly(0));
    _mockBookService.Verify(s=>s.GetByISBN(sample.ISBN),Times.Exactly(0));
  }

  [Fact]
  [Description("BK-05 : Mise à jour des informations d'un livre existant")]
  public void UpdateValidBook()
  {
    //Arrange
    string key = "valid-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    //Act 
    _bookManager.PatchBook(sample);

    //Assert
    _mockBookService.Verify(s=>s.Update(sample),Times.Exactly(1));
    _mockBookService.Verify(s=>s.GetByISBN(sample.ISBN),Times.Exactly(0));
  }

  [Fact]
  [Description("BK-06 : Mise à jour du nombre de copies")]
  public void AddBookCopies()
  {
    //Arrange
    string key = "valid-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    //Act 
    _bookManager.AddBookCopies(sample.ISBN,2);

    //Assert
    _mockBookService.Verify(s=>s.AddCopies(sample.ISBN,2),Times.Exactly(1));
    _mockBookService.Verify(s=>s.GetByISBN(sample.ISBN),Times.Exactly(0));
  }


  [Fact]
  [Description("BK-07 : Mise à jour d'un livre inexistant")]
  public void AddBookCopiesUnknownBook()
  {
    //Arrange
    string key = "missing-ISBN-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    _mockBookService.Setup(s=>s.AddCopies(sample.ISBN,2)).Returns(()=>throw new BookNotFoundException());

    //Act 
    BookNotFoundException exception = Assert.Throws<BookNotFoundException>(() => _bookManager.AddBookCopies(sample.ISBN,2));

    //Assert
    _mockBookService.Verify(s=>s.AddCopies(sample.ISBN,2),Times.Exactly(1));
  }

  [Fact]
  [Description("BK-08 : Suppression d'un livre existant sans emprunts actifs")]
  public void DeleteBookWithoutLoan()
  {

    //Arrange
    string key = "valid-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    _mockBookService.Setup(s=>s.HasActiveLoan(sample.BookId)).Returns(false);

    //Act 
    bool result = _bookManager.DeleteBook(sample.BookId);


    //Assert
    Assert.True(result);
    _mockBookService.Verify(s=>s.Delete(sample.BookId),Times.Exactly(1));
  }

  [Fact]
  [Description("BK-09 : Tentative de suppression d'un livre actuellement emprunté")]
  public void DeleteBookWithtLoan()
  {

    //Arrange
    string key = "valid-book"; 
    Book sample = _faker[key]??throw new Exception($"Missing configurations: Book[{key}]");

    _mockBookService.Setup(s=>s.HasActiveLoan(sample.BookId)).Returns(true);
    _mockBookService.Setup(s=>s.Delete(sample.BookId)).Returns(()=>throw new InvalidBookOperation());

    //Act 
    InvalidBookOperation exception = Assert.Throws<InvalidBookOperation>(() => _bookManager.DeleteBook(sample.BookId));

    //Assert
    _mockBookService.Verify(s=>s.Delete(sample.BookId),Times.Exactly(1));
  }

}
