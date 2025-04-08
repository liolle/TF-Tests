namespace bookmanager.services;

public interface ILogger 
{
  void Log(string message);
}

public class Logger : ILogger
{
    public void Log(string message)
    {
        throw new NotImplementedException();
    }
}
