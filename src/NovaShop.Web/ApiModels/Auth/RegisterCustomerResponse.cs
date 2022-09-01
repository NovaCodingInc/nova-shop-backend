namespace NovaShop.Web.ApiModels.Auth;

public class RegisterCustomerResponse
{
    public bool Succeeded { get; set; }
    public List<RegisterCustomerErrorResponse> Errors { get; set; }

    public RegisterCustomerResponse(bool succeeded)
    {
        Succeeded = succeeded;
        Errors = new List<RegisterCustomerErrorResponse>();
    }
}

public class RegisterCustomerErrorResponse
{
    public string Code { get; set; }
    public string Description { get; set; }

    public RegisterCustomerErrorResponse(string code, string description)
    {
        Code = code;
        Description = description;
    }
}