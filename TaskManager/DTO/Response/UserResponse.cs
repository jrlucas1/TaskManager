namespace TaskManager.DTO.Response
{
    public class UserResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
