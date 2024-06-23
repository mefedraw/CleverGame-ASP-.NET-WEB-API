namespace clever.Core.Abstractions;

using clever.Core.Models;

public interface IUserQuestsRepository
{
    bool UserExists(string tgId);
    Task IncreaseQuestsNumber(string tgId, int completedAmount);

    int GetUserCompletedQuestsAmount(string tgId);
}