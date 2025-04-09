namespace __Tests__.faker;
using bookmanager.models;

public class Faker<T>
where T : Book
{

  Dictionary<string, Book> _books = new Dictionary<string, Book>
  {
    {
      "valid-book",
        new Book()
        {
          BookId = "B-9780132350884-1",
          Title = "Clean Code", 
          Author = "Robert C. Martin", 
          ISBN = "9780132350884", 
          Year = 2008, 
          Genre = "Programming", 
          Copies = 3
        }
    }
  };

  public Book? this[string key]
  {
    get
    {
      return _books[key];
    }
  }
}
