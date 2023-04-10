using System.Net;
using Duende.IdentityServer.Services;
using IdServer.Db.Models;
using IdServer.Db.RepositoryServices.InvitationRepository;
using IdServer.Db.RepositoryServices.OrganizationRepository;
using IdServer.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdServer.Controllers;

[ApiController]
[Route("[controller]")]
public class ConnectController : ControllerBase
{
    private readonly ILogger<ConnectController> _logger;
    private readonly IIdentityServerInteractionService _interactionService;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly UserManager<AppUser> _userManager;

    public ConnectController(
        IIdentityServerInteractionService interactionService,
        SignInManager<AppUser> signInManager,
        IOrganizationRepository organizationRepository,
        IInvitationRepository invitationRepository,
        UserManager<AppUser> userManager,
        ILogger<ConnectController> logger)
    {
        _interactionService = interactionService;
        _signInManager = signInManager;
        _organizationRepository = organizationRepository;
        _invitationRepository = invitationRepository;
        _userManager = userManager;
        _logger = logger;  // TODO add logs
    }

    [HttpGet("login")]
    public void Login([FromQuery] string returnUrl)
    {
        // TODO this endpoint should be available only without auth
        Response.Redirect($"https://localhost:44484/identity/login?returnUrl={returnUrl}");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        // TODO this endpoint should be available only without auth
        var returnUrl = "https://localhost:4200/";
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, loginDto.IsPersistent, false);

        return result.Succeeded ? Ok(new { returnUrl }) : BadRequest();
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout([FromQuery] string logoutId, CancellationToken cancellationToken)
    {
        // TODO this endpoint should be available only for the respective auth user
        var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
    
        if (logoutRequest == null || (logoutRequest.ShowSignoutPrompt && User.Identity?.IsAuthenticated == true))
        {
            return Ok(new { prompt = User.Identity?.IsAuthenticated ?? false });
        }
    
        await _signInManager.SignOutAsync();

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> PostLogout([FromQuery] string logoutId, CancellationToken cancellationToken)
    {
        // TODO this endpoint should be available only for the respective auth user
        await _interactionService.GetLogoutContextAsync(logoutId);

        await _signInManager.SignOutAsync();

        return Ok();
    }
    
    [HttpPost("register/{invitationCode}/invitation/{orgIdentifier}/organization")]
    public async Task RegisterWithInvitation([FromBody] RegisterRequestDto registerRequestDto, [FromQuery] Guid invitationCode, [FromQuery] Guid orgIdentifier, CancellationToken cancellationToken)
    {
        // TODO this endpoint should be available without auth
        var invitationEntity = await _invitationRepository.GetByIdentifier(invitationCode, cancellationToken);
        if (invitationEntity is null)
        {
            // this user was not invited
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        var organizationEntity = await _organizationRepository.GetByIdentifier(orgIdentifier, cancellationToken);
        
        var user = new AppUser
        {
            UserName = registerRequestDto.Email,
            Email = registerRequestDto.Email,
            OrganizationEntity = organizationEntity
        };
        
        var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
    }
    
    [HttpPost("register")]
    public async Task CreateOrganizationAndRegister([FromBody] RegisterRequestDto registerRequestDto, CancellationToken cancellationToken)
    {
        // TODO this endpoint should be available without auth
        var organizationEntity = await _organizationRepository.CreateOrganization(cancellationToken);
        
        var user = new AppUser
        {
            UserName = registerRequestDto.Email,
            Email = registerRequestDto.Email,
            OrganizationEntity = organizationEntity
        };
        
        await _userManager.CreateAsync(user, registerRequestDto.Password);
    }
}
