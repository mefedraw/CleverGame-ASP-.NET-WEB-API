namespace clever.Contracts;

using clever.Core.Models;

public record GetAvailableTaskResponse(
    List<TasksInfo> AvailableTasks
);