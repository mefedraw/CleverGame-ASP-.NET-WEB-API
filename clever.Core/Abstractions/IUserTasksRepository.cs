namespace clever.Core.Abstractions;
using clever.Core.Models;

public interface IUserTasksInfoRepository
{
    Task AddTask(short taskId, int profit, string text, TaskType type, ulong workload, string link);

    Task DeleteTask(short taskId);

    TasksInfo GetTaskInfo(short taskId);
}