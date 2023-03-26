using System.Net;
using IdServer.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdServer.Controllers;

[ApiController]
[Route("[controller]")]
public class UserClaimsController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<UserClaimsController> _logger;

    public UserClaimsController(
        UserManager<AppUser> userManager,
        ILogger<UserClaimsController> logger)
    {
        _userManager = userManager;
        _logger = logger;  // TODO add logs
    }
    
    [HttpGet("organization_id")]
    public async Task<Guid> GetUserOrganization([FromQuery] string userId)
    {
        // TODO should get user_id from id_token
        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null) return user.OrganizationEntity.Identifier;
        
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Guid.Empty;
    }

    [HttpGet("role")]
    public async Task<string> GetUserRole([FromQuery] string userId)
    {
        // TODO should get user_id from id_token
        var user = await _userManager.FindByIdAsync(userId);
        var roles = await _userManager.GetRolesAsync(user);
        return roles.First();
    }
}