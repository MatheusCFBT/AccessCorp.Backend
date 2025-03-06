using AccessCorp.Application.Entities;
using AccessCorp.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace AccessCorp.Application.Services;

public class SendEmailService : ISendEmailService
{
    private readonly SendEmail _sendEmail;

    public SendEmailService(IOptions<SendEmail> sendEmail)
    {
        _sendEmail = sendEmail.Value;
    }
    
    public void SendEmail(string toEmail, string token)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_sendEmail.FromName, _sendEmail.FromEmail));
        email.To.Add(new MailboxAddress("", toEmail));
        email.Subject = "Token de acesso";

        email.Body = new TextPart(TextFormat.Plain) { Text = token };

        using (var smtp = new SmtpClient())
        {
            smtp.Connect(_sendEmail.SmtpServer, _sendEmail.SmtpPort, false);
            smtp.Authenticate(_sendEmail.FromEmail, _sendEmail.FromPassword); 
            smtp.Send(email); 
            smtp.Disconnect(true);  
        }
    }
}