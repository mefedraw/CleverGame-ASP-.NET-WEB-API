namespace clever.Contracts;

public record UserPointsResponse(
    string TgId,
    ulong Points
);


public record GetTopUsersPointsResponse(
    string TopUser1TgId,
    ulong TopUser1Points,
    string TopUser2TgId,
    ulong TopUser2Points,
    string TopUser3TgId,
    ulong TopUser3Points
);