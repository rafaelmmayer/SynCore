using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SynCore.Api.Features.Posts;

namespace SynCore.Api.Controllers.Posts;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
[AllowAnonymous]
public class PostsController : Controller
{
    private readonly ISender _sender;

    public PostsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts(CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetAllPosts.Command(), cancellationToken);

        return Ok(res);
    }
}