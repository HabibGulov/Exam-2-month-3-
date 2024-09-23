public interface ITaskAttachmentRepository
{
    Task<TaskAttachment?> GetAttachmentByIdAsync(Guid id);
    Task<IEnumerable<TaskAttachment>> GetAllAttachmentsAsync();
    Task<bool> AddAttachmentAsync(TaskAttachment attachment);
    Task<bool> UpdateAttachmentAsync(TaskAttachment attachment);
    Task<bool> DeleteAttachmentAsync(Guid id);
}