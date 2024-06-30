using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clever.Core.Models;

public enum TaskType
{
    Follow = 1,
    Quests,
    Referrals,
    Friends,
    Balance
}

public class TasksInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short TaskId { get; set; }
    public int Profit { get; set; }
    
    [StringLength(64)]
    public string Text { get; set; }
    public TaskType Type { get; set; }
    public ulong Workload { get; set; }
    public string Link { get; set; }

    public TasksInfo(int profit, string text, TaskType type, ulong workload, string link)
    {
        Profit = profit;
        Text = text;
        Type = type;
        Workload = workload;
        Link = link;
    }

    public TasksInfo() { }
}

// dotnet ef migrations add UserFundsTable --project TgAppCrates.DataAccess