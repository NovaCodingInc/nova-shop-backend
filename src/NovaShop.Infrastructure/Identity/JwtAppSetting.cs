namespace NovaShop.Infrastructure.Identity;

public class JwtAppSetting
{
    public string Secret { get; set; } = string.Empty;
    public int Expiration { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string ValidAt { get; set; } = string.Empty;
}