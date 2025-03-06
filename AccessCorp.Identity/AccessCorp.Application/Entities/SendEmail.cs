namespace AccessCorp.Application.Entities;

public class SendEmail
{
    public string SmtpServer  { get; set; }
    public int SmtpPort  { get; set; }
    public string FromEmail { get; set; }
    public string FromPassword { get; set; }
    public string FromName { get; set; }    
}