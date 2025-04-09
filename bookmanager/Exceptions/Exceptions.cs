namespace bookmanager.exceptions;


public class ISBNDuplicateException : Exception 
{
  public ISBNDuplicateException () : base("Duplicate ISBN")
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
