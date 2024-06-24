namespace clever.Controllers;

using Microsoft.AspNetCore.Mvc;
using clever.Core.Abstractions;
using clever.Contracts;

[ApiController]
[Route("api/v1/points")]
public class UserPointsController : ControllerBase
{
    private readonly IUserPointsRepository _userPointsRepository;

    public UserPointsController(IUserPointsRepository userPointsRepository)
    {
        _userPointsRepository = userPointsRepository;
    }

    [HttpGet("users-amount")]
    public Task<ActionResult<UserPointsResponse>> GetUserPoints([FromQuery] string tgId)
    {
        var pointsAmount = _userPointsRepository.GetUserPoints(tgId);
        var response = new UserPointsResponse(tgId, pointsAmount);
        return Task.FromResult<ActionResult<UserPointsResponse>>(Ok(response));
    }

    [HttpGet("top-users")]
    public async Task<ActionResult<GetTopUsersPointsResponse>> GetTopUsersPoints()
    {
        var topUsers = _userPointsRepository.GetTopUsersPoints();

        Console.WriteLine(topUsers.Item1.TgId + " " + topUsers.Item1.Points);
        Console.WriteLine(topUsers.Item2.TgId + " " + topUsers.Item2.Points);
        Console.WriteLine(topUsers.Item3.TgId + " " + topUsers.Item3.Points);

        var response = new GetTopUsersPointsResponse(
            topUsers.Item1.TgId, topUsers.Item1.Points,
            topUsers.Item2.TgId, topUsers.Item2.Points,
            topUsers.Item3.TgId, topUsers.Item3.Points); ;
        return Ok(response);
    }

    [HttpPatch("add")]
    public async Task<ActionResult> AddPointsToUser([FromQuery] string tgId, [FromQuery] ulong fundsAmount)
    {
        await _userPointsRepository.AddPointsToUser(tgId, fundsAmount);
        return Created();
    }

    [HttpPatch("remove")]
    public async Task<ActionResult<bool>> RemovePointsFromUser([FromQuery] string tgId, [FromQuery] ulong fundsAmount)
    {
        var response = await _userPointsRepository.RemovePointsFromUser(tgId, fundsAmount);
        return Ok(response);
    }
}