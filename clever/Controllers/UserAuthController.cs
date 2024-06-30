namespace clever.Controllers;

using Microsoft.AspNetCore.Mvc;
using clever.Core.Abstractions;

[ApiController]
[Route("api/v1/auth")]
public class UserAuthController : ControllerBase
{
    private readonly IUserAuthRepository _userAuthRepository;
    private readonly IUserPointsRepository _userPointsRepository;
    private readonly IUserQuestsRepository _userQuestsRepository;

    public UserAuthController(IUserAuthRepository userAuthRepository, IUserPointsRepository userPointsRepository,
        IUserQuestsRepository userQuestsRepository)
    {
        _userAuthRepository = userAuthRepository;
        _userPointsRepository = userPointsRepository;
        _userQuestsRepository = userQuestsRepository;
    }

    [HttpPost("user")]
    public async Task<ActionResult> AuthUser([FromQuery] string tgId, [FromQuery] string tgUsername)
    {
        await _userAuthRepository.AuthUser(tgId, tgUsername);
        return CreatedAtAction(nameof(AuthUser), new { tgId }, null);
    }

    [HttpGet("exists")]
    public Task<ActionResult<bool>> UserExists([FromQuery] string tgId)
    {
        var userExists = _userAuthRepository.UserExists(tgId); // returns bool
        return Task.FromResult<ActionResult<bool>>(userExists);
    }

    [HttpPost("user-full-auth")]
    public async Task<ActionResult> UserFullAuth([FromQuery] string tgId, [FromQuery] string tgUsername)
    {
        await _userAuthRepository.AuthUser(tgId, tgUsername);
        await _userPointsRepository.AddPointsToUser(tgId, 0);
        await _userQuestsRepository.IncreaseQuestsNumber(tgId, 0);
        return Created();
    }
}