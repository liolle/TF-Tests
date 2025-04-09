namespace bookmanager.models;

public class Book
{
  public required string BookId {get;set;} 
  public string Title {get;set;} = "";
  public string Author {get;set;} = "";
  public string ISBN {get;set;} = "";
  public int Year {get;set;}
  public string Genre {get;set;} = "";
  public int Copies {get;set;}

  public Book(Book book)
  {
    BookId = book.BookId;
    Title = book.Title;
    Author = book.Author;
    ISBN = book.ISBN;
    Year = book.Year;
    Genre = book.Genre;
    Copies = book.Copies;
  }

  public Book()
  {

  }
}
