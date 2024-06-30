namespace clever.Core.Abstractions;
using clever.Core.Models;

public interface IUserTaskRepository
{
    Task<List<TasksInfo>> GetAvailableTasks(string tgId);

    Task InitTasks(string tgId);

    Task MarkAsDone(string tgId,short taskId);
}