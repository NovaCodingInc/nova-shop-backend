using NovaShop.ApplicationCore.Interfaces;

namespace NovaShop.Infrastructure.Services;

public class EmailService : IEmailService
{
    public Task SendMailAsync(string email, string subject, string message)
    {
        throw new NotImplementedException();
    }
}