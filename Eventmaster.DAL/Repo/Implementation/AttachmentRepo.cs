namespace Eventmaster.DAL.Repo.Implementation
{
    public class AttachmentRepo :IAttachmentRepo
    {
        private readonly Context _db;
        public AttachmentRepo(Context db)
        {
            _db = db;
        }
        public void Add(Attachments attachment)
        {
            _db.Attachments.Add(attachment);
            _db.SaveChanges();
        }
        public Attachments GetById(int attachmentId)
        {
            return _db.Attachments.Find(attachmentId);
        }

    }
}
