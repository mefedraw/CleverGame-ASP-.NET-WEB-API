using clever.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using clever.DataAccess;
using clever.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(AppDbContext)));
    });
 builder.Services.AddScoped<IUserPointsRepository, UserPointsRepository>();
 builder.Services.AddScoped<IUserQuestsRepository, UserQuestsRepository>();
 builder.Services.AddScoped<IUserTasksInfoRepository, UserTasksInfoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();