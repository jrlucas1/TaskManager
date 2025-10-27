using TaskManager.Models.Enums;

namespace TaskManager.DTO.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = "#000000";
        public TaskPriority Priority { get; set; }
        public TaskSituation Status { get; set; }
    }
}
