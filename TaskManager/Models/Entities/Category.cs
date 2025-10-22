using TaskManager.Models.Enums;

namespace TaskManager.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color {  get; set; } = "#000000";
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
        public TaskSituation Status { get; set; } = TaskSituation.Pending;
        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
