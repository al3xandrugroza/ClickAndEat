using Duende.IdentityServer;
using IdServer.Db.Models;
using IdServer.Db.RepositoryServices.InvitationRepository;
using IdServer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdServer.Controllers;

[ApiController]
[Route("identity/[controller]")]
public class InvitationController : ControllerBase
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly UserManager<AppUser> _userManager;

    public InvitationController(IInvitationRepository invitationRepository, UserManager<AppUser> userManager)
    {
        _invitationRepository = invitationRepository;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<InvitationDto> InviteUser([FromBody] AddInvitationRequestDto addInvitationRequestDto, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(addInvitationRequestDto.Email) is not null) throw new Exception("conflict");

        var organizationId = HttpContext.User.Claims.Single(c => c.Type.Contains("org_id")).Value;
        return await _invitationRepository.CreateInvitation(addInvitationRequestDto.Email, addInvitationRequestDto.Role, new Guid(organizationId), cancellationToken);
    }

    [HttpGet]
    public async Task<InvitationDto> GetInvitation([FromQuery] string invitationCode, CancellationToken cancellationToken)
    {
        return await _invitationRepository.GetByIdentifier(new Guid(invitationCode), cancellationToken);
    }
}