namespace clever.Core.Models;

public enum TaskType
{
    Follow = 1,
    Quests,
    Referrals,
    Friends,
    Balance
}

public class TasksInfo(Guid id, short taskId, int profit, string text, TaskType type, ulong workload, string link)
{
    public Guid Id { get; set; } = id;
    public short TaskId { get; set; } = taskId;
    public int Profit { get; set; } = profit;
    public string Text { get; set; } = text;
    public TaskType Type { get; set; } = type;
    public ulong Workload { get; set; } = workload;
    public string Link { get; set; } = link;
}

// dotnet ef migrations add UserFundsTable --project TgAppCrates.DataAccess