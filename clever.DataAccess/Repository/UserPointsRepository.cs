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
            var tempUser = new UserPoints(tgId, amount);
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

    public (UserPoints, UserPoints, UserPoints) GetTopUsersPoints()
    {
        var allUsers = _context.DbPoints.OrderByDescending(u => u.Points).ToList();
        var topUsers = (allUsers[0], allUsers[1], allUsers[2]);

        Console.WriteLine($"top1user: {allUsers[0].TgId}  \" \" points: {allUsers[0].Points}");
        Console.WriteLine($"top2user: {allUsers[1].TgId}  \" \" points: {allUsers[1].Points}");
        Console.WriteLine($"top3user: {allUsers[2].TgId}  \" \" points: {allUsers[2].Points}");
        Console.WriteLine("end of repo GetTopUsersPoints logs");

        return topUsers;
    }

    public ulong GetUserPoints(string tgId)
    {
        if (_context.DbPoints.Count(u => u.TgId == tgId) == 0)
        {
            return 0;
        }

        var user = _context.DbPoints.Single(u => u.TgId == tgId);
        return user.Points;
    }

    public ulong GetUserPlaceDuePoints(string tgId)
    {
        var userPoints = GetUserPoints(tgId);
        int higherScoresCount = _context.DbPoints.Count(u => u.Points > userPoints);
        return (ulong)(higherScoresCount + 1);
    }
}