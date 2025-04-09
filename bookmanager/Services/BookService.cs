using bookmanager.models;

namespace bookmanager.services; 

public interface IBookService
{
  bool Add(Book book);
  Book GetByISBN(string ISBN);
  bool Update(Book book);
  bool AddCopies(string ISBN, int quantity);
  bool HasActiveLoan(string bookId);
  bool Delete(string bookId);

}

public class BooService : IBookService
{
    public bool Add(Book book)
    {
        throw new NotImplementedException();
    }

    public bool AddCopies(string ISBN, int quantity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(string bookId)
    {
        throw new NotImplementedException();
    }

    public Book GetByISBN(string ISBN)
    {
        throw new NotImplementedException();
    }

    public bool HasActiveLoan(string bookId)
    {
        throw new NotImplementedException();
    }

    public bool Update(Book book)
    {
        throw new NotImplementedException();
    }
}



