public class TaskAttachmentsAndUser
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public string FilePath { get; set; } = null!; 
    public DateTime CreatedAt { get; set; }
    public Guid UserId{get; set;}
    public string UserName{get; set;}=null!;
    public string Email{get; set;}=null!;
}