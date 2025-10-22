using TaskManager.Models.Enums;

namespace TaskManager.Models.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskSituation Situation { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public User User { get; set; } = null!;
        public Category? Category { get; set; }
    }
}
