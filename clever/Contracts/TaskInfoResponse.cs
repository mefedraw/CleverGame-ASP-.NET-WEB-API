using clever.Core.Models;

namespace clever.Contracts;

public record TaskInfoResponse(
    short TaskId,
    int Profit,
    string Text,
    int Type,
    ulong Workload,
    string Link
);