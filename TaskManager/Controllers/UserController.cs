using Microsoft.AspNetCore.Identity;
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
    public class UserController : Controller
    {

        private readonly TaskManagerContext _context;

        public UserController(TaskManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> GetTasks()
        {
            var users = await _context.Users
                .Include(t => t.TaskItems)
                .Select(t => new UserResponse
                {
                    ID = t.ID,
                    Name = t.Name,
                    Email = t.Email,
                    CreatedAt = t.createdAt,
                })
                .ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponse>> GetTaskById(int id)
        {
            var user = await _context.Users
                .Include(t => t.TaskItems)
                .Where(t => t.ID == id)
                .Select(t => new UserResponse
                {
                    ID = t.ID,
                    Name = t.Name,
                    Email = t.Email,
                    CreatedAt = t.createdAt,
                })
                .FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponse>> AddNewUser(UserRequest userRequest)
        {
            var userExists = await _context.Users.AnyAsync(u => u.ID == userRequest.Id);

            if (userExists)
            {
                return NotFound(new { message = "Usuário já existe!" });
            }
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                PasswordHash = hasher.HashPassword(null, userRequest.Password),
                createdAt    = DateTime.UtcNow,
                TaskItems = null
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var createdUser = await _context.Users
                            .Where(u => u.ID == user.ID)
                            .Select(u => new UserResponse
                            {
                                ID = u.ID,
                                Name = u.Name,
                                Email = u.Email,
                                CreatedAt = u.createdAt,
                            })
                            .FirstAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = createdUser.ID }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UserRequest userRequest)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new { message = "User não encontrado" });

            var hasher = new PasswordHasher<User>();

            user.Name = userRequest.Name;
            user.Email = userRequest.Email;
            user.PasswordHash = hasher.HashPassword(null, userRequest.Password);


            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new { message = "User não encontrado" });


            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
