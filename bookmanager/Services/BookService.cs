using bookmanager.models;

namespace bookmanager.services; 

public interface IBookService
{
  bool Add(Book book);
  Book GetById(string bookId);

}

public class BooService : IBookService
{
    public bool Add(Book book)
    {
        throw new NotImplementedException();
    }

    public Book GetById(string bookId)
    {
        throw new NotImplementedException();
    }
}



