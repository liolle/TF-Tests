namespace vowel;

public interface IPurchase
{
  void Save(Purchase p);
}

public class Purchase : IPurchase
{

  public int UserId {get;set;}
  public required string Product {get;set;}
  public int Amount {get;set;}

  public void Save(Purchase p)
  {
    throw new NotImplementedException();
  }
}

public class PurchaseManager 
{
  private readonly IPurchase _repo;
  private readonly IEmailService _emailService;
  private readonly ILogger _logger;

  public PurchaseManager(IPurchase repo, IEmailService emailService, ILogger logger)
  {
    _repo = repo;
    _emailService = emailService;
    _logger = logger;
  }


  public bool RegisterPurchase(Purchase p)
  {
    _repo.Save(p);
    try
    {
      _emailService.SendConfirmationEmail(p.UserId,p);
        
    }
    catch (Exception e)
    {
      _logger.Log(e.Message);
        
    }
    return true;
  }
}
