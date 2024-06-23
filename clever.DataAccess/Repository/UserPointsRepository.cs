using Microsoft.EntityFrameworkCore;
using clever.Core.Models;
using clever.Core.Abstractions;

namespace clever.DataAccess.Repository;

public class UserPointsRepository : IUserPointsRepository
{
    private readonly AppDbContext _context;

    public UserPointsRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool UserExists(string tgId)
    {
        return _context.DbPoints.Any(u => u.TgId == tgId);
    }

    public async Task AddPointsToUser(string tgId, ulong amount)
    {
        var user = _context.DbPoints.SingleOrDefault(u => u.TgId == tgId);
        if (user == null)
        {
            var tempUser = new UserPoints(Guid.NewGuid(), tgId, amount);
            await _context.DbPoints.AddAsync(tempUser);
            Console.WriteLine("No UserPoints at all, UserPoints were added!");
        }
        else
        {
            user.Points += amount;
            _context.Update(user);
            Console.WriteLine("Points were added" + "(" + amount + ")" + " to user " + tgId);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemovePointsFromUser(string tgId, ulong amount)
    {
        if (!UserExists(tgId)) return false;
        var user = _context.DbPoints.SingleOrDefault(u => u.TgId == tgId);
        if (user.Points < amount) return false;
        user.Points -= amount;
        _context.Update(user);
        await _context.SaveChangesAsync();
        Console.WriteLine("Funds were removed" + "(" + amount + ")" + " from user " + tgId);
        return true;
    }

    public (UserPoints, UserPoints, UserPoints) GetTopUsersPoints ()
    {
        var allUsers = _context.DbPoints.OrderBy(u => u.Points).ToList();
        var topUsers = (allUsers[0], allUsers[1], allUsers[2]);
        return topUsers;
    }

    public ulong GetUserPoints(string tgId)
    {
        var user = _context.DbPoints.Single(u => u.TgId == tgId);
        return user.Points;
    }
}