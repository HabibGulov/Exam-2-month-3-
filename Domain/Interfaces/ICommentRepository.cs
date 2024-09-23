public interface ICommentRepository
{
    Task<Comment?> GetCommentByIdAsync(Guid id);
    Task<IEnumerable<Comment>> GetAllCommentsAsync();
    Task<bool> AddCommentAsync(Comment comment);
    Task<bool> UpdateCommentAsync(Comment comment);
    Task<bool> DeleteCommentAsync(Guid id);
}