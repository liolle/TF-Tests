using bookmanager.models;

namespace bookmanager.services; 

public interface IBookService
{
  bool Add(Book book);
  Book GetByISBN(string ISBN);

}

public class BooService : IBookService
{
    public bool Add(Book book)
    {
        throw new NotImplementedException();
    }

    public Book GetByISBN(string ISBN)
    {
        throw new NotImplementedException();
    }
}



