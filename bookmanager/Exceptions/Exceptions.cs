namespace bookmanager.exceptions;


public class ISBNDuplicateException : Exception 
{
  public ISBNDuplicateException () : base("Duplicate ISBN")
  {
  }
}

public class BookNotFoundException : Exception 
{
  public BookNotFoundException () : base($"Book not found")
  {
  }
}

public class InvalidBookOperation : Exception
{
  public InvalidBookOperation() : base()
  {

  }
}

public class EmptyFieldException : Exception
{ 
  public List<string> MissingFields { get; }

  public EmptyFieldException(List<string> fields) 
    : base($"Missing field{(fields.Count>0?"s":"")}: {string.Join(", ", fields)}")
  {
    MissingFields = fields ?? [];
  }
}

public class InvalidFieldException : Exception
{ 
  public List<string> InvalidFields { get; }

  public InvalidFieldException(List<string> fields) 
    : base($"Invalid value for field{(fields.Count>0?"s":"")}: {string.Join(", ", fields)}")
  {
    InvalidFields = fields ?? [];
  }
}

