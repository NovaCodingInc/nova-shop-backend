namespace NovaShop.ApplicationCore.Interfaces;

public interface IEmailService
{
    Task SendMailAsync(string email, string subject, string message);
}