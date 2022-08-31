namespace NovaShop.Infrastructure.Identity.Users;

public class ApplicationUser : IdentityUser
{
    public DateTime CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}