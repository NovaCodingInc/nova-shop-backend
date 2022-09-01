using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using NovaShop.Infrastructure.Identity;
using NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities;
using NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.Utilities;
using NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization;

namespace NovaShop.Web.Configurations;

public static class IdentitySetup
{
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

        var appSettingsSection = configuration.GetSection("JwtAppSetting");
        services.Configure<JwtAppSetting>(appSettingsSection);

        var appSettings = appSettingsSection.Get<JwtAppSetting>();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            // JWT Setup
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.ValidAt,
                ValidIssuer = appSettings.Issuer
            };
        });

        services.AddHttpContextAccessor();
        services.AddSingleton<IClaimBasedAuthorizationUtilities, ClaimBasedAuthorizationUtilities>();
        services.AddSingleton<IMvcUtilities, MvcUtilities>();
        services.AddScoped<IAuthorizationHandler, ClaimBasedAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            options.AddPolicy("ClaimBasedAuthorization", policy =>
                policy.Requirements.Add(new ClaimBasedAuthorizationRequirement()));
        });
    }
}