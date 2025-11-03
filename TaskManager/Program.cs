using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models.Entities;
using TaskManager.Repositories.Implementations;
using TaskManager.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TaskManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBaseRepository<TaskItem>, TaskItemRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
