namespace NovaShop.Web.ApiModels.Auth;

public class LoginCustomerResponse
{
    public bool Succeeded { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public LoginCustomerResponse(bool succeeded)
    {
        Succeeded = succeeded;
    }
}