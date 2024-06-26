namespace clever.Controllers;

using Microsoft.AspNetCore.Mvc;
using clever.Core.Abstractions;
using clever.Core.Models;
using clever.Contracts;

[ApiController]
[Route("api/v1/tasks-info")]
public class UserTasksController : ControllerBase
{
    private readonly IUserTasksInfoRepository _userTasksInfoRepository;

    public UserTasksController(IUserTasksInfoRepository userTasksInfoRepository)
    {
        _userTasksInfoRepository = userTasksInfoRepository;
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddTask([FromQuery] short taskId, [FromQuery] int profit, [FromQuery] string text,
        [FromQuery] TaskType type, [FromQuery] ulong workload, [FromQuery] string link)
    {
        await _userTasksInfoRepository.AddTask(taskId, profit, text, type, workload, link);
        return Created();
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteTask([FromQuery] short taskId)
    {
        await _userTasksInfoRepository.DeleteTask(taskId);
        return Created();
    }

    [HttpGet("info")]
    public Task<ActionResult<TaskInfoResponse>> GetTaskInfo([FromQuery] short taskId)
    {
        var tempTask = _userTasksInfoRepository.GetTaskInfo(taskId);
        var response = new TaskInfoResponse(taskId, tempTask.Profit, tempTask.Text, (int)tempTask.Type, tempTask.Workload, tempTask.Link);
        // не понятно надо передавать int или TaskType
        return Task.FromResult<ActionResult<TaskInfoResponse>>(Ok(response));
    }
}