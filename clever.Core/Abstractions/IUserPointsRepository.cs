namespace clever.Core.Abstractions;

using clever.Core.Models;

public interface IUserPointsRepository
{
    Task AddPointsToUser(string tgId, ulong amount);
    Task<bool> RemovePointsFromUser(string tgId, ulong amount);
    bool UserExists(string tgId);
    (UserPoints, UserPoints, UserPoints) GetTopUsersPoints();
    ulong GetUserPoints(string tgId);
    ulong GetUserPlaceDuePoints(string tgId);
}