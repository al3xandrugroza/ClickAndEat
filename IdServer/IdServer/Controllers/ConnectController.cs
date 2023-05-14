using System.Net;
using Duende.IdentityServer.Services;
using IdServer.Db.Models;
using IdServer.Db.RepositoryServices.InvitationRepository;
using IdServer.Db.RepositoryServices.OrganizationRepository;
using IdServer.Dtos;
using IdServer.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdServer.Controllers;

[ApiController]
[Route("[controller]")]
public class ConnectController : ControllerBase
{
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
        UserManager<AppUser> userManager)
    {
        _interactionService = interactionService;
        _signInManager = signInManager;
        _organizationRepository = organizationRepository;
        _invitationRepository = invitationRepository;
        _userManager = userManager;
    }

    [HttpGet("login")]
    public void Login([FromQuery] string returnUrl)
    {
        Response.Redirect($"https://localhost:44484/login");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var returnUrl = "https://localhost:4200/dashboard";
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, loginDto.IsPersistent, false);

        return result.Succeeded ? Ok(new { returnUrl }) : BadRequest();
    }

    [HttpGet("logout")]
    public async Task Logout([FromQuery] string logoutId, CancellationToken cancellationToken)
    {
        var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
    
        if (logoutRequest == null)
        {
            return;
        }
    
        await _signInManager.SignOutAsync();

        Response.Redirect("https://localhost:4200");
    }

    [HttpPost("register/invitation/{invitationCode:guid}")]
    public async Task RegisterWithInvitation([FromBody] RegisterByInvitationDto registerByInvitationDto, Guid invitationCode, CancellationToken cancellationToken)
    {
        var invitationEntity = await _invitationRepository.GetByIdentifier(invitationCode, cancellationToken);
        var organizationEntity = await _organizationRepository.GetByIdentifier(invitationEntity.OrganizationId, cancellationToken);

        var user = new AppUser
        {
            UserName = invitationEntity.Email,
            Email = invitationEntity.Email,
            OrganizationEntity = organizationEntity
        };

        var result = await _userManager.CreateAsync(user, registerByInvitationDto.Password);
        if (!result.Succeeded) throw new Exception();

        result = await _userManager.AddToRoleAsync(user, invitationEntity.Role);
        if (!result.Succeeded) throw new Exception();

        await _invitationRepository.DeleteAllInvitationForEmailInAllOrganizations(invitationEntity.Email, cancellationToken);
    }

    [HttpPost("register")]
    public async Task CreateOrganizationAndRegister([FromBody] RegisterOrgRequestDto registerOrgRequestDto, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(registerOrgRequestDto.Email) is not null)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            return;
        }

        var organizationEntity = await _organizationRepository.CreateOrganization(registerOrgRequestDto.OrganizationName, cancellationToken);

        var user = new AppUser
        {
            UserName = registerOrgRequestDto.Email,
            Email = registerOrgRequestDto.Email,
            OrganizationEntity = organizationEntity
        };

        var result = await _userManager.CreateAsync(user, registerOrgRequestDto.Password);
        if (!result.Succeeded) throw new Exception("Error on user creation");

        result = await _userManager.AddToRoleAsync(user, RoleType.Admin);
        if (!result.Succeeded) throw new Exception("Error on user role assignment");
    }
}
