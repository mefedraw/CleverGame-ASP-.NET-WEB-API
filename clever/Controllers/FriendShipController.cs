

using Microsoft.AspNetCore.Mvc;
using clever.Contracts;
using clever.Core.Abstractions;
using FriendResponse = clever.Core.Models.FriendResponse;

namespace clever.Controllers;

[ApiController]
[Route("api/v1/friends")]
public class FriendShipController : ControllerBase
{
    
    private readonly IFriendShipRepository _friendShipRepository;

    public FriendShipController(IFriendShipRepository friendShipRepository)
    {
        _friendShipRepository = friendShipRepository;
    }

    [HttpPost("send-request")]
    public async Task<IActionResult> SendFriendRequest(string userTgId, string friendTgId)
    {
        await _friendShipRepository.SendFriendRequest(userTgId, friendTgId);
        return Ok("Friend request sent successfully.");
    }

    [HttpPost("accept-request")]
    public async Task<IActionResult> AcceptFriendRequest(string userTgId, string friendTgId)
    {
        await _friendShipRepository.AcceptFriendRequest(userTgId, friendTgId);
        return Ok("Friend request accepted successfully.");
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveFriend(string userTgId, string friendTgId)
    {
        await _friendShipRepository.RemoveFriend(userTgId, friendTgId);
        return Ok("Friend removed successfully.");
    }

    [HttpGet("list")]
    public async Task<ActionResult<FriendListResponse>> FriendsList(string userTgId)
    {
        List<FriendResponse> friendsList = await _friendShipRepository.FriendsList(userTgId);
        var response = new FriendListResponse(friendsList);
        return Ok(response);
    }

    [HttpGet("requests")]
    public async Task<ActionResult<FriendRequestsResponse>> FriendRequestsList(string userTgId)
    {
        var friendRequestsList = await _friendShipRepository.FriendRequestsList(userTgId);
        var response = new FriendRequestsResponse(friendRequestsList);
        return Ok(response);
    }
}