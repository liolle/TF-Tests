namespace vowel;

public interface IFileManager
{
  bool create(string path, string content);
  bool delete(string path);
  bool exist(string path);
  string read(string path);
}

public class FileManager : IFileManager
{
  private const string DIRECTORY = "../test_files";

  public bool create(string path, string content)
  {
    try
    {
      File.WriteAllText(path,content);
      return true;
    }
    catch (Exception)
    {
      return false;
      throw;
    }
  }

  public bool delete(string path)
  {
    try
    {
      File.Delete(path);
      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }

    public bool exist(string path)
    {
      return File.Exists(path);
    }

    public string read(string path)
  {
    try
    {
      return File.ReadAllText(path);
    }
    catch (Exception)
    {
      return "";
    }
  }
}
