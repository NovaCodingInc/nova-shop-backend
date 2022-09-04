namespace NovaShop.Web.ApiModels.Auth;

public class CheckAuthCustomerResponse
{
    public bool Succeeded { get; set; }

    public string? Email { get; set; } = string.Empty;

    public CheckAuthCustomerResponse(bool succeeded)
    {
        Succeeded = succeeded;
    }
}