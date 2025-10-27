using TaskManager.Models.Enums;

namespace TaskManager.DTO.Request
{
    public class CategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = "#000000";
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
        public TaskSituation Status { get; set; } = TaskSituation.Pending;
    }
}
