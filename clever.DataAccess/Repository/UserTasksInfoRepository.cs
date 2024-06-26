using clever.Core.Abstractions;
using clever.Core.Models;

namespace clever.DataAccess.Repository;

public class UserTasksInfoRepository : IUserTasksInfoRepository
{
    private readonly AppDbContext _context;

    public UserTasksInfoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTask(short taskId, int profit, string text, TaskType type, ulong workload, string link)
    {
        if (_context.DbTasksInfo.Count(u => u.TaskId == taskId) == 0)
        {
            var tempTask = new TasksInfo(Guid.NewGuid(), taskId, profit, text, type, workload, link);
            await _context.DbTasksInfo.AddAsync(tempTask);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteTask(short taskId)
    {
        var tempTask = _context.DbTasksInfo.SingleOrDefault(u => u.TaskId == taskId);
        _context.DbTasksInfo.Remove(tempTask);
        await _context.SaveChangesAsync();
    }

    public TasksInfo GetTaskInfo(short taskId)
    {
        var tempTask = _context.DbTasksInfo.SingleOrDefault(u => u.TaskId == taskId);
        return tempTask;
    }
}