using TaskManager.Models.Enums;

namespace TaskManager.DTO.Response
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public DateTime DueDate {  get; set; }
        public TaskPriority Priority { get; set; } 
        public TaskSituation Situation { get; set; }
        public DateTime CreatedAt {  get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int? CategoryId {  get; set; }
        public string CategoryName {  get; set; }
    }
}
