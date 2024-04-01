using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SynCore.Api.Controllers.Classes.Requests;
using SynCore.Api.Features.Classes;
using SynCore.Api.Services;

namespace SynCore.Api.Controllers.Classes;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClassesController : Controller
{
    private readonly ISender _sender;
    private readonly AuthUser _authUser;

    public ClassesController(ISender sender, AuthUserProvider authUserProvider)
    {
        _sender = sender;
        _authUser = authUserProvider.GetAuthUser();
    }

    [HttpPost]
    public async Task<IActionResult> AddClass([FromBody] AddClassRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<AddClass.Command>();

        command.UserId = _authUser.Id;
        
        var res = await _sender.Send(command, cancellationToken);
        
        return CreatedAtAction(nameof(GetClassById), new { id = res.Id }, res);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClasses(CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetAllClasses.Command(), cancellationToken);

        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetClassById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetClassById.Command() { Id = id }, cancellationToken);

        return Ok(res);
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateClass([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _sender.Send(new DeactivateClass.Command() { Id = id }, cancellationToken);

        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteClass([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _sender.Send(new DeleteClass.Command() { Id = id }, cancellationToken);

        return Ok();
    }
}