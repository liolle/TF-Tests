using bookmanager.models;

namespace bookmanager.services; 


public interface IBookManagerger
{
  bool AddBook(Book book);

}

public class BookManager : IBookManagerger 
{
  private readonly IBookService _bookService;
  private readonly IUserService _userService;

  public BookManager(IBookService bookService,IUserService userService)
  {
    _bookService = bookService;
    _userService = userService;
  }

    public bool AddBook(Book book)
    {
      return _bookService.Add(book);
    }
}
