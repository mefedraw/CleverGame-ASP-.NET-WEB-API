namespace clever.Core.Models;

public class UserQuests
{
    public UserQuests(Guid id, string tgId, int completed)
    {
        Id = id;
        TgId = tgId;
        Completed = completed;
    }

    public Guid Id { get; set; }
    public string TgId { get; set; }
    public int Completed { get; set; }
}