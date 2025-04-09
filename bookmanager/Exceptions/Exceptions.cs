namespace bookmanager.exceptions;


public class ISBNDuplicateException : Exception 
{
  public ISBNDuplicateException () : base("Duplicate ISBN")
  {
  }
}
