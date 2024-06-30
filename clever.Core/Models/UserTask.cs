using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clever.Core.Models;

public class UserTask
{
    public UserTask(string tgId, short taskId, bool isCompleted)
    {
        TgId = tgId;
        TaskId = taskId;
        IsCompleted = isCompleted;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("UserAuth")] public string TgId { get; set; }

    public virtual UserAuth UserAuth { get; set; }

    [ForeignKey("TasksInfo")] public short TaskId { get; set; }

    public virtual TasksInfo TasksInfo { get; set; }

    public bool IsCompleted { get; set; }
}