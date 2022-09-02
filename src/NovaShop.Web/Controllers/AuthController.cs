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
        var response = new RegisterCustomerResponse(false);

        if (!ModelState.IsValid || User.Identity!.IsAuthenticated)
            return BadRequest(response);

        var user = new ApplicationUser()
        {
            Email = request.Email,
            UserName = request.Email,
            CreateDate = DateTime.Now,
            EmailConfirmed = false,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            // TODO: Maybe you want send confirm email here

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

    #region login

    [HttpPost("Login")]
    [ProducesResponseType(typeof(LoginCustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Login user",
        Description = "Login user",
        OperationId = "Auth.Login",
        Tags = new[] { "Auth" })
    ]
    public async Task<ActionResult<LoginCustomerResponse>> Login(LoginCustomerRequest request)
    {
        var response = new LoginCustomerResponse(false);

        if (!ModelState.IsValid || User.Identity!.IsAuthenticated)
            return BadRequest(response);

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return BadRequest(response);

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, request.RememberMe);

        if (signInResult.Succeeded)
        {
            response.Succeeded = true;
            response.Email = request.Email;
            response.Token = await _jwtService.GenerateJwt(user.Email);

            return Ok(response);
        }

        return BadRequest(response);
    }

    #endregion
}