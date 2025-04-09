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
  public void AddDuplicateBookExpectException()
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
}
