using clever.Core.Models;
using clever.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace clever.DataAccess.Repository;

public class FriendShipRepository : IFriendShipRepository
{
    private readonly AppDbContext _context;

    public FriendShipRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task SendFriendRequest(string userTgId, string friendTgId)
    {
        if (_context.DbFriendships.Count(u => u.UserId == userTgId && u.FriendId == friendTgId) == 0 &&
            _context.DbFriendships.Count(u => u.UserId == friendTgId && u.FriendId == userTgId) == 0)
        {
            var userFriendRelation = new FriendShip(Guid.NewGuid(), userTgId, friendTgId, false,
                DateOnly.FromDateTime(DateTime.Today));
            var friendUserRelation = new FriendShip(Guid.NewGuid(), friendTgId, userTgId, false,
                DateOnly.FromDateTime(DateTime.Today));

            await _context.DbFriendships.AddAsync(friendUserRelation); // request of user1 goes to user2

            await _context.SaveChangesAsync();
        }
    }

    public async Task AcceptFriendRequest(string userTgId, string friendTgId)
    {
        var friendRequestExists = _context.DbFriendships.Count(u =>
            u.UserId == userTgId && u.FriendId == friendTgId && u.FriendRequestAccepted == false);

        if (friendRequestExists == 1)
        {
            var userFriendRelation = _context.DbFriendships.Single(u =>
                u.UserId == userTgId && u.FriendId == friendTgId && u.FriendRequestAccepted == false);
            var friendUserRelation = new FriendShip(Guid.NewGuid(), friendTgId, userTgId, true,
                DateOnly.FromDateTime(DateTime.Today));
            _context.DbFriendships.AddAsync(friendUserRelation);
            userFriendRelation.FriendRequestAccepted = true;
            userFriendRelation.FriendsDate = DateOnly.FromDateTime(DateTime.Today);
            _context.Update(userFriendRelation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFriend(string userTgId, string friendTgId)
    {
        var userFriendRelation =
            _context.DbFriendships.SingleOrDefault(u =>
                u.UserId == userTgId && u.FriendId == friendTgId);
        var friendUserRelation =
            _context.DbFriendships.SingleOrDefault(u =>
                u.UserId == friendTgId && u.FriendId == userTgId);
        _context.DbFriendships.Remove(userFriendRelation);
        _context.DbFriendships.Remove(friendUserRelation);
        await _context.SaveChangesAsync();
    }

    public async Task<List<FriendResponse>> FriendsList(string userTgId)
    {
        var friendsList = await _context.DbUserAuth
            .Join(_context.DbFriendships, dua => dua.TgId, fr => fr.FriendId, (dua, fr) => new { dua, fr })
            .Join(_context.DbPoints, x => x.dua.TgId, dp => dp.TgId, (x, dp) => new { x.dua, x.fr, dp })
            .Join(_context.DbQuests, x => x.dua.TgId, dq => dq.TgId, (x, dq) => new { x.dua, x.fr, x.dp, dq })
            .Where(x => x.fr.FriendRequestAccepted == true && x.fr.UserId == userTgId)
            .OrderBy(x => x.fr.FriendsDate)
            .Select(x => new FriendResponse(x.dua.TgUsername,x.dp.Points,x.dq.Completed))
            .ToListAsync();

        foreach (var friend in friendsList)
        {
            Console.WriteLine($"FRIENDS INFO: {friend.Username}, {friend.Points}, {friend.Completed}");
        }

        return friendsList;
    }



    public async Task<List<string>> FriendRequestsList(string userTgId)
    {
        var friendRequestsList = await (
            from dua in _context.DbUserAuth
            join fr in _context.DbFriendships on dua.TgId equals fr.UserId
            where fr.FriendRequestAccepted == false && fr.UserId == userTgId
            orderby fr.FriendsDate descending 
            select new
            {
                dua.TgUsername,
            }
        ).ToListAsync();
        
        return friendRequestsList.Select(f => (f.TgUsername)).ToList();
    }
}