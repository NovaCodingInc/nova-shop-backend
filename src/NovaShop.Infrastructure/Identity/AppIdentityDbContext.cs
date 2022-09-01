using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NovaShop.Infrastructure.Identity;

// Add-Migration InitIdentityContext -Context AppIdentityDbContext -o "Identity\Migrations"
// Update-Database -Context AppIdentityDbContext
public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}