using Microsoft.AspNetCore.Mvc;
using clever.Core.Abstractions;
using clever.Contracts;

namespace clever.Controllers;

[ApiController]
[Route("api/v1/tasks")]
public class UserTaskController : ControllerBase
{
    private readonly IUserTaskRepository _userTaskRepository;

    public UserTaskController(IUserTaskRepository userTaskRepository)
    {
        _userTaskRepository = userTaskRepository;
    }
    
    [HttpGet("available")]
    public async Task<ActionResult<GetAvailableTaskResponse>> GetAvailableTasks([FromQuery] string tgId)
    {
        var availableTasks = await _userTaskRepository.GetAvailableTasks(tgId);
        var response = new GetAvailableTaskResponse(availableTasks);
        return Ok(response);
    }

    [HttpPost("user-init")]
    public async Task<ActionResult> InitTasks([FromQuery] string tgId)
    {
        await _userTaskRepository.InitTasks(tgId);
        return NoContent();
    }
}