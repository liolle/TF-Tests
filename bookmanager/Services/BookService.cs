using bookmanager.models;

namespace bookmanager.services; 

public interface IBookService
{
  bool Add(Book book);

}

public class BooService : IBookService
{
    public bool Add(Book book)
    {
        throw new NotImplementedException();
    }
}



