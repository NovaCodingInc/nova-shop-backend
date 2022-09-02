namespace NovaShop.Web.ApiModels.Auth;

public class LoginCustomerRequest
{
    [Required]
    [MaxLength(300)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}