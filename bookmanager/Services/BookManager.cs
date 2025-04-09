using bookmanager.exceptions;
using bookmanager.models;

namespace bookmanager.services; 


public interface IBookManagerger
{
  bool AddBook(Book book);
  bool PatchBook(Book book);
  bool AddBookCopies(string ISBN,int quantity);
  bool DeleteBook(string bookId);

}

public partial class BookManager : IBookManagerger 
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
    CheckBookField(book);
    // Propagating Unique key violation
    if(_bookService.GetByISBN(book.ISBN) is not null){throw new ISBNDuplicateException();}

    _bookService.Add(book);
    return true;
  }

  public bool AddBookCopies(string ISBN, int quantity)
  {
    _bookService.AddCopies(ISBN,quantity);
    return true;
  }

    public bool DeleteBook(string bookId)
    {
      _bookService.Delete(bookId);
      return true;
    }

    public bool PatchBook(Book book)
  {
    _bookService.Update(book);
    return true;
  }
}

// Helper methods
public partial class BookManager
{
  private void CheckBookField(Book book)
  {
    // check mandatory fiels 
    List<string> missing_fields = [];
    if(String.IsNullOrEmpty(book.Title)){missing_fields.Add("Title");}
    if(String.IsNullOrEmpty(book.ISBN)){missing_fields.Add("ISBN");}

    if (missing_fields.Count>0){throw new EmptyFieldException(missing_fields);}

    // check invalid fiels
    List<string> invalid_fields = [];
    if(book.Copies<=0){invalid_fields.Add("Copies");}

    if (invalid_fields.Count>0){throw new InvalidFieldException(invalid_fields);}
  }

}
