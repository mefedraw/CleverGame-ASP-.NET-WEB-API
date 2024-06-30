using clever.Core.Models;

namespace clever.Contracts;

public record TaskInfoResponse(
    int Profit,
    string Text,
    TaskType Type,
    ulong Workload,
    string Link
);