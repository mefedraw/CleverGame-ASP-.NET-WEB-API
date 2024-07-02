namespace clever.Contracts;

public record FriendListResponse
(
    List<Core.Models.FriendResponse> FriendsList
);

public record FriendResponse(string Username, ulong Points, int Quests);


public record FriendRequestsResponse
(
    List<string> FriendRequests
);