using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clever.Core.Models;

public class FriendResponse
{
    public FriendResponse(string username, ulong points, int completed)
    {
        Username = username;
        Points = points;
        Completed = completed;
    }

    public string Username { get; set; }
    public ulong Points { get; set; }

    public int Completed { get; set; }
}

public class FriendShip
{
    public FriendShip(Guid id, string userId, string friendId, bool friendRequestAccepted, DateOnly friendsDate)
    {
        Id = id;
        UserId = userId;
        FriendId = friendId;
        FriendRequestAccepted = friendRequestAccepted;
        FriendsDate = friendsDate;
    }

    public Guid Id { get; set; }

    [ForeignKey("UserAuth")]
    [StringLength(30)]
    public string UserId { get; set; }

    [ForeignKey("UserAuth")]
    [StringLength(30)]
    public string FriendId { get; set; }

    public bool FriendRequestAccepted { get; set; }

    public DateOnly FriendsDate { get; set; }
}