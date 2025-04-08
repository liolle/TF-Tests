using Moq;
using vowel;

namespace __Tests__;

public class PurchaseManagerTest 
{
  private readonly Mock<IPurchase> _mockRepo;
  private readonly Mock<IEmailService> _mockEmail;
  private readonly Mock<ILogger> _mockLogger;
  private readonly PurchaseManager _service;

  private readonly Purchase sample_product = new()
  {
    UserId = 1, //
           Product = "Pomme",
           Amount = 42
  };

  public PurchaseManagerTest()
  {
    _mockRepo = new Mock<IPurchase>();
    _mockEmail = new Mock<IEmailService>();
    _mockLogger = new Mock<ILogger>();
    _service = new PurchaseManager(_mockRepo.Object,_mockEmail.Object,_mockLogger.Object);
  }

  [Fact]
  public void SavePurchase()
  {
    bool succeed = _service.RegisterPurchase(sample_product);
    
    Assert.True(succeed);
    _mockRepo.Verify(r=>r.Save(sample_product), Times.Once);
  }

  [Fact]
  public void SendEmailAfterRegistration()
  {
    _service.RegisterPurchase(sample_product);
    _mockEmail.Verify(r=>r.SendConfirmationEmail(sample_product.UserId,sample_product), Times.Once);
  }


  [Fact]
  public void LogErrorButStillSavePurchase(){

    _mockEmail.Setup(r=>r.SendConfirmationEmail(sample_product.UserId,sample_product)).Returns(()=>throw new Exception("SMTP Error"));

    _service.RegisterPurchase(sample_product);
    
    _mockRepo.Verify(r=>r.Save(sample_product), Times.Once);
    _mockLogger.Verify(r=>r.Log(It.Is<string>(msg=>msg.Contains("SMTP Error"))), Times.Once);
  }
}
