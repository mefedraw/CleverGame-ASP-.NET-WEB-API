using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clever.Core.Models;

public class UserPoints(string tgId, ulong points)
{
    [Key, ForeignKey("UserAuth")]
    [StringLength(30)]
    public string TgId { get; set; } = tgId;
    public ulong Points { get; set; } = points;

    public virtual UserAuth UserAuth { get; set; }
}