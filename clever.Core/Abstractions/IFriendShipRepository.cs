namespace clever.Core.Abstractions;
using clever.Core.Models;
public interface IFriendShipRepository
{
    Task SendFriendRequest(string userTgId, string friendTgId);
    Task AcceptFriendRequest(string userTgId, string friendTgId);
    Task RemoveFriend(string userTgId, string friendTgId);
    Task<List<FriendResponse>> FriendsList(string userTgId);

    Task<List<string>> FriendRequestsList(string userTgId);
}