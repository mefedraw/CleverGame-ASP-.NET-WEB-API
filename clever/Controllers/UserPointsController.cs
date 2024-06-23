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

    [HttpGet("Users-amount")]
    public Task<ActionResult<UserPointsResponse>> GetCards([FromQuery] string tgId)
    {
        var pointsAmount = _userPointsRepository.GetUserPoints(tgId);
        var response = new UserPointsResponse(tgId, pointsAmount);
        return Task.FromResult<ActionResult<UserPointsResponse>>(Ok(response));
    }
    
    [HttpGet("Top-Users")]
    public Task<ActionResult<UserPointsResponse>> GetCards()
    {
        var topUsers = _userPointsRepository.GetTopUsersPoints();
        ((string TgId, ulong Points), (string TgId, ulong Points), (string TgId, ulong Points)) topUsersList = ((topUsers.Item1.TgId, topUsers.Item1.Points),
            (topUsers.Item2.TgId, topUsers.Item2.Points), (topUsers.Item3.TgId, topUsers.Item3.Points));
        var response = new GetTopUsersPointsResponse(topUsersList);
        return Task.FromResult<ActionResult<UserPointsResponse>>(Ok(response));
    }
    
    [HttpPatch("Add")]
    public async Task<ActionResult> AddPointsToUser([FromQuery] string tgId, [FromQuery] ulong fundsAmount)
    {
        await _userPointsRepository.AddPointsToUser(tgId, fundsAmount);
        return Created();
    }
    
    [HttpPatch("Remove")]
    public async Task<ActionResult<bool>> RemovePointsFromUser([FromQuery] string tgId, [FromQuery] ulong fundsAmount)
    {
        var response =await _userPointsRepository.RemovePointsFromUser(tgId, fundsAmount);
        return Ok(response);
    }
}