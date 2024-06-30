using Microsoft.EntityFrameworkCore;
using clever.Core.Models;
using clever.Core.Abstractions;

namespace clever.DataAccess.Repository;

public class UserQuestsRepository : IUserQuestsRepository 
{
    private readonly AppDbContext _context;

    public UserQuestsRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool UserExists(string tgId)
    {
        return _context.DbQuests.Any(u => u.TgId == tgId);
    }

    public async Task IncreaseQuestsNumber(string tgId, int completedAmount)
    {
        if (UserExists(tgId))
        {
            var tempUser = _context.DbQuests.Single(u => u.TgId == tgId);
            tempUser.Completed += completedAmount;
            _context.Update(tempUser);
        }
        else
        {
            var tempUser = new UserQuests(tgId, completedAmount);
            await _context.AddAsync(tempUser);
        }

        await _context.SaveChangesAsync();
    }

    public int GetUserCompletedQuestsAmount(string tgId)
    {
        var completedAmount = _context.DbQuests.Single(u => u.TgId == tgId).Completed;
        return completedAmount;
    }
}