using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SynCore.Api.Features.Auth;

namespace SynCore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromForm] SignUp.Command command, CancellationToken cancellationToken)
    {
        var user = await _sender.Send(command, cancellationToken);

        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromForm] Login.Command command, 
        CancellationToken cancellationToken)
    {
        var user = await _sender.Send(command, cancellationToken);
        
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Email),
            new(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
            new("college", user.CollegeName),
        };
        
        var identity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return Ok(user);
    }
}