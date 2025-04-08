namespace vowel;

public interface IEmailService 
{
  bool SendConfirmationEmail(int userId, Purchase product);

}

public class EmailService : IEmailService
{
    public bool SendConfirmationEmail(int userId, Purchase content)
    {
        throw new NotImplementedException();
    }
}
