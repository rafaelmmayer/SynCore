using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SynCore.Api.Controllers.Posts.Requests;
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

    [HttpGet] // GetAllPosts
    public async Task<IActionResult> GetAllPosts(CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetAllPosts.Command(), cancellationToken);

        return Ok(res);
    }
   
    [HttpPost] // AddPost
    public async Task<IActionResult> AddPost([FromBody] AddPostRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<AddPost.Command>();

        var res = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetPostById), new { id = res.Id }, res);
    }

    [HttpGet("{id:guid}")] // GetPostById
    public async Task<IActionResult> GetPostById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetPostById.Command() { Id = id }, cancellationToken);

        return Ok(res);
    }

    [HttpDelete("{id:guid}")] // DeletePost
    public async Task<IActionResult> DeletePost([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _sender.Send(new DeletePost.Command() { Id = id }, cancellationToken);

        return Ok();
    }
}