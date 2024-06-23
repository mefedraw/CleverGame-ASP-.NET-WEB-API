using clever.Core.Abstractions;
using clever.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace clever.Controllers;

[ApiController]
[Route("api/v1/quests")]
public class UserQuestsController : ControllerBase
{
    private readonly IUserQuestsRepository _userQuestsRepository;

    public UserQuestsController(IUserQuestsRepository userQuestsRepository)
    {
        _userQuestsRepository = userQuestsRepository;
    }

    [HttpPatch("increase")]
    public async Task<ActionResult> IncreaseQuestsNumber([FromQuery] string tgId, [FromQuery] int completedAmount)
    {
        await _userQuestsRepository.IncreaseQuestsNumber(tgId, completedAmount);
        return Created();
    }

    [HttpGet("completed")]
    public Task<ActionResult> GetUserCompletedQuestsAmount([FromQuery] string tgId)
    {
        var userCompletedQuestsAmount = _userQuestsRepository.GetUserCompletedQuestsAmount(tgId);
        var response = new UserQuestsResponse(userCompletedQuestsAmount);
        return Task.FromResult<ActionResult>(Ok(response));
    }
}