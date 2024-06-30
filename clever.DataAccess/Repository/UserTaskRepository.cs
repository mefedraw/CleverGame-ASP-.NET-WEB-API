using Microsoft.EntityFrameworkCore;

namespace clever.DataAccess.Repository;

using clever.Core.Abstractions;
using clever.Core.Models;

public class UserTaskRepository : IUserTaskRepository
{
    private readonly AppDbContext _context;

    public UserTaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TasksInfo>> GetAvailableTasks(string tgId)
    {
        var uncompletedTasks = await _context.DbTasksInfo
            .Where(task => !_context.DbUserTask
                .Any(ut => ut.TgId == tgId && ut.TaskId == task.TaskId && ut.IsCompleted))
            .ToListAsync();

        return uncompletedTasks;
    }


    
    public async Task InitTasks(string tgId)
    {
        var tasks = _context.DbTasksInfo.ToList();

        foreach (var task in tasks)
        {
            var userTask = new UserTask(tgId, task.TaskId, false);
            await _context.DbUserTask.AddAsync(userTask);
        }

        await _context.SaveChangesAsync();
    }
}