using bookmanager.models;
using bookmanager.services;
using Moq;

namespace __Tests__;

public class BookManagerTest 
{
  private readonly Mock<IBookService> _mockBookService;
  private readonly Mock<IUserService> _mockUserService;
  private readonly BookManager _bookManager;

  public BookManagerTest ()
  {
    _mockBookService = new();
    _mockUserService = new();
    _bookManager = new (_mockBookService.Object,_mockUserService.Object);
  }

  private readonly Book sample_book = new()
  {
   Title = "", 
   Author = "", 
   ISBN = "", 
   Year = 2008, 
   Genre = "", 
  };


  [Fact]
  public void AddBookExpectTrue()
  {
    
    _bookManager.AddBook(sample_book);

    _mockBookService.Verify(s=>s.Add(sample_book),Times.Once);
  }
}
