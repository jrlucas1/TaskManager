using TaskManager.Models.Enums;

namespace TaskManager.DTO.Request
{
    public class CreateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
        public TaskSituation Situation { get; set; } = TaskSituation.Pending;
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
