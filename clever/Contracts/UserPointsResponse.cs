namespace clever.Contracts;

public record UserPointsResponse(
    string TgId,
    ulong Points
);

public record GetTopUsersPointsResponse(
    ((string TgId, ulong Points), (string TgId, ulong Points), (string TgId, ulong Points)) TopUsers
);