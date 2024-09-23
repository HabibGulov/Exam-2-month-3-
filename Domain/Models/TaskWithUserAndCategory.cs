public class TaskWithUserAndCategory
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!; 
    public string Email { get; set; } = null!; 
    public string PasswordHash { get; set; } = null!; 
    public Guid CategotyId { get; set; }
    public string Name { get; set; } = null!; 
    public DateTime CategoryCreatedAt { get; set; }
}