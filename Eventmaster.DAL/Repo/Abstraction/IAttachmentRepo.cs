namespace Eventmaster.DAL.Repo.Abstraction
{
    public interface IAttachmentRepo
    {
        void Add(Attachments attachment);
        Attachments GetById(int attachmentId);
    }
}
