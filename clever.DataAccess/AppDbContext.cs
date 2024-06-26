using Microsoft.EntityFrameworkCore;
using clever.Core.Models;

namespace clever.DataAccess;

public class AppDbContext: Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserPoints> DbPoints { get; set; }  
    public DbSet<UserQuests> DbQuests { get; set; }
    
    public DbSet<TasksInfo> DbTasksInfo { get; set; }
    
}