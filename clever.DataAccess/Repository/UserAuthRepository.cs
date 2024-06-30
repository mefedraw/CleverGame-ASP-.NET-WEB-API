using clever.Core.Models;

namespace clever.DataAccess.Repository;
using clever.Core.Abstractions;

public class UserAuthRepository : IUserAuthRepository
{
    private readonly AppDbContext _context;

    public UserAuthRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AuthUser(string tgId, string tgUsername)
    {
        var tempUser = new UserAuth(tgId, tgUsername, DateOnly.FromDateTime(DateTime.UtcNow));
        await _context.DbUserAuth.AddAsync(tempUser);
        await _context.SaveChangesAsync();
    }
    
    public bool UserExists(string tgId)
    {
        var tempUser = _context.DbUserAuth.SingleOrDefault(u => u.TgId == tgId);
        return tempUser != null;
    }
}