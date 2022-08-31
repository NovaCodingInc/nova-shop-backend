namespace NovaShop.Infrastructure.Identity.Services;

public interface IJwtService
{
    Task<string> GenerateJwt(string email);
}