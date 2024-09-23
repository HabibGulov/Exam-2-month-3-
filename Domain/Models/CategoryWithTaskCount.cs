public class CategoryWithTaskCount
{
    public Guid Id{get; set;}
    public string Name{get; set;}=null!;
    public DateTime CreatedAt{get; set;}
    public int TaskCount{get; set;}
}