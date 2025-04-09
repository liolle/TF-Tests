using bookmanager.exceptions;
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
  private readonly ILogger _logger;

  public BookManager(IBookService bookService,IUserService userService,ILogger logger)
  {
    _bookService = bookService;
    _userService = userService;
    _logger = logger;
  }

  public bool AddBook(Book book)
  {
    // Propagating Unique key violation
    if(_bookService.GetById(book.BookId) is not null){throw new ISBNDuplicateException();}

    _bookService.Add(book);
    return true;
  }
}
