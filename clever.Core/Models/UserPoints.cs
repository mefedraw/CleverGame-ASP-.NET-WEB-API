namespace clever.Core.Models;

public class UserPoints
{
    
    public UserPoints(Guid id, string tgId, ulong points)
    {
        Id = id;
        TgId = tgId;
        Points = points;
    }
    public Guid Id { get; set; }
    public string TgId { get; set; }
    public ulong Points{ get; set; }
}