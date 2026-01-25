namespace Eventmaster.DAL.Entity
{
    public class Attachments
    {
        //attachment_id(PK), event_id(FK), file_path, file_type
        //file_id (PK), event_id (FK), file_url, file_name, upload_date
        [Key]
        public int FileId { get; set; }
        public int EventId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public Event Event { get; set; }
        public Attachments() { }
        public Attachments(int eventId, string filePath, string fileName, DateTime uploadDate)
        {
            EventId = eventId;
            FilePath = filePath;
            FileName = fileName;
            UploadDate = uploadDate;
        }
    }
}
