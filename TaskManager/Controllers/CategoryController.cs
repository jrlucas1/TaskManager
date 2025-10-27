using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTO.Request;
using TaskManager.DTO.Response;
using TaskManager.Models.Entities;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {

        private readonly TaskManagerContext _context;

        public CategoryController(TaskManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> GetTasks()
        {
            var categories = await _context.Categories
                .Include(c => c.TaskItems)
                .Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Color = c.Color,
                    Priority = c.Priority,
                    Status = c.Status,
                })
                .ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponse>> GetTaskById(int id)
        {
            var category = await _context.Categories
                .Include(c => c.TaskItems)
                .Where(c => c.Id == id)
                .Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Color = c.Color,
                    Priority = c.Priority,
                    Status = c.Status,
                })
                .FirstOrDefaultAsync();
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponse>> AddNewUser(CategoryRequest categoryRequest)
        {
            var categoryExists = await _context.Categories.AnyAsync(u => u.Id == categoryRequest.Id);

            if (categoryExists)
            {
                return NotFound(new { message = "Categoria já existe!" });
            }

            var category = new Category
            {
               Name = categoryRequest.Name,
               Description = categoryRequest.Description,
               Color = categoryRequest.Color,
               Priority = categoryRequest.Priority,
               Status = categoryRequest.Status,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var createdCategory = await _context.Categories
                            .Where(c => c.Id == category.Id)
                            .Select(c => new CategoryResponse
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Description = c.Description,
                                Color = c.Color,
                                Priority = c.Priority,
                                Status = c.Status,
                            })
                            .FirstAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, CategoryRequest categoryRequest)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            category.Name = categoryRequest.Name;
            category.Description = categoryRequest.Description;
            category.Color = categoryRequest.Color;
            category.Priority = categoryRequest.Priority;
            category.Status = categoryRequest.Status;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });


            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
