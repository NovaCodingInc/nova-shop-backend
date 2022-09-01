namespace NovaShop.Web.Controllers;

public class AuthController : ApiBaseController
{
    #region constructor

    private readonly IMediator _mediator;
    private readonly IJwtService _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(IMediator mediator,
        IJwtService jwtService, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _mediator = mediator;
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #endregion

    #region register

    [HttpPost("Register")]
    [ProducesResponseType(typeof(RegisterCustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Register user",
        Description = "Register user",
        OperationId = "Auth.Register",
        Tags = new[] { "Auth" })
    ]
    public async Task<ActionResult<RegisterCustomerResponse>> Register([FromBody] RegisterCustomerRequest request)
    {
        if (!ModelState.IsValid || _signInManager.IsSignedIn(User)) 
            return BadRequest();

        var response = new RegisterCustomerResponse(false);

        var user = new ApplicationUser()
        {
            Email = request.Email,
            UserName = request.Email.Split("@")[0].Split(".")[0],
            CreateDate = DateTime.Now,
            EmailConfirmed = true,
        };

        // TODO: Maybe you want send confirm email here

        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            response.Succeeded = true;
            await _mediator.Send(new RegisterCustomerCommand(user.Id));
            return Ok(response);
        }

        foreach (var error in result.Errors)
        {
            response.Errors.Add(new RegisterCustomerErrorResponse(error.Code, error.Description));
        }

        return BadRequest(response);
    }

    #endregion
}