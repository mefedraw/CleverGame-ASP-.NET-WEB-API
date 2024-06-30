using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clever.Core.Models;

public class UserAuth(string tgId, string tgUsername, DateOnly authDate)
{
    [Key]
    [StringLength(30)]
    public string TgId { get; set; } = tgId;

    public string TgUsername { get; set; } = tgUsername;

    public DateOnly AuthDate { get; set; } = authDate;
}