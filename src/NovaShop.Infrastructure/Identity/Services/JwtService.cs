namespace NovaShop.Infrastructure.Identity.Services;

public class JwtService : IJwtService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtAppSetting _appSettings;

    public JwtService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtAppSetting> appSettings)
    {
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    public async Task<string> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.ValidAt,
            Expires = DateTime.UtcNow.AddHours(_appSettings.Expiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}