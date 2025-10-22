using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using TaskManager.Data;
using TaskManager.DTO.Request;
using TaskManager.DTO.Response;
using TaskManager.Models.Entities;
using TaskManager.Models.Enums;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : Controller
    {

        private readonly TaskManagerContext _context;

        public TaskItemController(TaskManagerContext context) 
        {
             _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(t => t.User)
                .Include(t => t.Category)
                .Select(t => new TaskResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Situation = t.Situation,
                    CreatedAt = t.CreatedAt,
                    UserId = t.UserId,
                    Username = t.User.Name,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category != null ? t.Category.Name : null
                })
                .ToListAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponse>> GetTaskById(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.User)
                .Include(t => t.Category)
                .Where(t => t.Id == id)
                .Select(t => new TaskResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Situation = t.Situation,
                    CreatedAt = t.CreatedAt,
                    UserId = t.UserId,
                    Username = t.User.Name,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category != null ? t.Category.Name : null
                })
                .FirstOrDefaultAsync();
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponse>> AddNewTask(CreateTaskRequest taskRequest)
        {
            var userExists = await _context.Users.AnyAsync(u => u.ID == taskRequest.UserId);

            if (!userExists)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == taskRequest.CategoryId);

            if (!categoryExists)
            {
                return NotFound(new { message = "Categoria não encontrado" });
            }

            var task = new TaskItem
            {
                Title = taskRequest.Title,
                Description = taskRequest.Description,
                DueDate = taskRequest.DueDate,
                Priority = taskRequest.Priority,
                Situation = taskRequest.Situation,
                CreatedAt = DateTime.UtcNow,
                UserId = taskRequest.UserId,
                CategoryId = taskRequest.CategoryId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var createdTask = await _context.Tasks
                            .Include(t => t.User)
                            .Include(t => t.Category)
                            .Where(t => t.Id == task.Id)
                            .Select(t => new TaskResponse
                            {
                                Id = t.Id,
                                Title = t.Title,
                                Description = t.Description,
                                DueDate = t.DueDate,
                                Priority = t.Priority,
                                Situation = t.Situation,
                                CreatedAt = t.CreatedAt,
                                UserId = t.UserId,
                                Username = t.User.Name,
                                CategoryId = t.CategoryId,
                                CategoryName = t.Category != null ? t.Category.Name : null
                            })
                            .FirstAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, CreateTaskRequest taskRequest)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound(new { message = "Task não encontrada" });

            task.Title = taskRequest.Title;
            task.Description = taskRequest.Description;
            task.DueDate = taskRequest.DueDate;
            task.Priority = taskRequest.Priority;
            task.Situation = taskRequest.Situation;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound(new { message = "Task não encontrada" });


            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
      
            return NoContent();
           
        }
    }
}
