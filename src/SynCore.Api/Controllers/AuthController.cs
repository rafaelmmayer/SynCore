﻿using System.Security.Claims;
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
    
    [HttpGet("cpf-exists")]
    public async Task<IActionResult> CpfExists([FromQuery] string cpf, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new CpfExists.Command() { Cpf = cpf }, cancellationToken);

        return Ok(res);
    }

    [HttpGet("email-exists")]
    public async Task<IActionResult> EmailExists([FromQuery] string email, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new EmailExists.Command() { Email = email }, cancellationToken);

        return Ok(res);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromForm] SignUp.Command command, CancellationToken cancellationToken)
    {
        var user = await _sender.Send(command, cancellationToken);

        return Ok(user);
    }
    
    [HttpPost("sign-in")]
    public async Task<IActionResult> Login(
        [FromForm] SignIn.Command command, 
        CancellationToken cancellationToken)
    {
        var user = await _sender.Send(command, cancellationToken);
        
        var claims = new List<Claim>()
        {
            new("id", user.Id.ToString()),
            new("email", user.Email),
            new("name", $"{user.Name} {user.LastName}"),
            new("college", user.CollegeName),
        };
        
        var identity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return Ok();
    }

    [HttpPost("sign-out")]
    public async Task<IActionResult> SignOutHandler()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        var claims = HttpContext.User.Claims
            .Select(c => new { c.Type, c.Value })
            .ToArray();

        return Ok(claims);
    }
    
}