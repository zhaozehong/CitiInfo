using System.Diagnostics;

namespace CityInfo.API.Services
{
  public class CouldMailService: IMailService
  {
    private string _mailFrom = "noreply@mycompany.com";
    private string _mailTo = "admin@mycompany.com";

    public void Send(string subject, string message)
    {
      Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with CouldMailService");
      Debug.WriteLine($"Subject: {subject}");
      Debug.WriteLine($"Message: {message}");
    }
  }
}
