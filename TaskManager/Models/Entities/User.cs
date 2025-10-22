namespace TaskManager.Models.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();

    }
}
