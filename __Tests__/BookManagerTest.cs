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

  public BookManagerTest ()
  {
    _mockBookService = new();
    _mockUserService = new();
    _mockLoggerService = new();
    _bookManager = new (_mockBookService.Object,_mockUserService.Object,_mockLoggerService.Object);
  }

  private readonly Book sample_book = new()
  {
    Title = "Clean Code", 
          Author = "Robert C. Martin", 
          ISBN = "9780132350884", 
          Year = 2008, 
          Genre = "Programming", 
  };

  [Fact]
  public void AddBookExpectTrue()
  {
    _bookManager.AddBook(sample_book);

    _mockBookService.Verify(s=>s.Add(sample_book),Times.Once);
  }

  [Fact]
  public void AddDuplicateBookExpectException()
  {
    _mockBookService.Setup(s=>s.Add(sample_book)).Returns(()=>throw new Exception("Dupplicate ISBN"));

    bool result =_bookManager.AddBook(sample_book);

    Assert.False(result);

    _mockBookService.Verify(s=>s.Add(sample_book),Times.Once());
    _mockLoggerService.Verify(r=>r.Log(It.Is<string>(msg=>msg.Contains("Dupplicate ISBN"))), Times.Exactly(1));
  }


  [Fact]
  public void AddBookMissingTitleExpectException()
  {
    _mockBookService.Setup(s=>s.Add(sample_book)).Returns(()=>throw new Exception("Missing Title"));

    bool result =_bookManager.AddBook(sample_book);

    Assert.False(result);

    _mockBookService.Verify(s=>s.Add(sample_book),Times.Once);
    _mockLoggerService.Verify(r=>r.Log(It.Is<string>(msg=>msg.Contains("Missing Title"))), Times.Exactly(1));
  }

  [Fact]
  public void AddBookMissingISBNExpectException()
  {
    _mockBookService.Setup(s=>s.Add(sample_book)).Returns(()=>throw new Exception("Missing ISBN"));

    bool result =_bookManager.AddBook(sample_book);

    Assert.False(result);

    _mockBookService.Verify(s=>s.Add(sample_book),Times.Once);
    _mockLoggerService.Verify(r=>r.Log(It.Is<string>(msg=>msg.Contains("Missing ISBN"))), Times.Exactly(1));
  }

}
