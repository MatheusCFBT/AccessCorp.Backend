namespace AccessCorp.Application.Interfaces;

public interface ISendEmailService
{
    public void SendEmail(string toEmail, string token);
}