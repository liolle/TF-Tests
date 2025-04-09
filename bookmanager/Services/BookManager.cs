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
    // check mandatory fiels 
    List<string> missing_fields = [];
    if(String.IsNullOrEmpty(book.Title)){missing_fields.Add("Title");}
    if(String.IsNullOrEmpty(book.ISBN)){missing_fields.Add("ISBN");}

    if (missing_fields.Count>0){throw new EmptyFieldException(missing_fields);}

    // Propagating Unique key violation
    if(_bookService.GetByISBN(book.ISBN) is not null){throw new ISBNDuplicateException();}

    _bookService.Add(book);
    return true;
  }
}
