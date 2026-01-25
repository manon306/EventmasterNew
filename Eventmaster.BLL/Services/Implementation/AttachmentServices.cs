using Eventmaster.BLL.ModelVM.Attachment;
using Microsoft.AspNetCore.Http;
namespace Eventmaster.BLL.Services.Implementation
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly IEventRepo _eventRepo;
        private readonly IAttachmentRepo _attachmentRepo;
        private readonly IMapper _mapper;
        public AttachmentServices(IEventRepo eventRepo, IAttachmentRepo attachmentRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _attachmentRepo = attachmentRepo;
            _mapper = mapper;
        }
        public async Task<(bool success, string message)> UploadAsync(int eventId, IFormFile file)
        {
            // 1. تحقق أن الحدث موجود
            Event ev;
            _eventRepo.GetEventById(eventId ,out  ev);
            if (ev == null)
                return (false, "Event not found");

            // 2. توليد اسم آمن للملف
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // 3. حفظ الملف فعليًا
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 4. حفظ البيانات في DB
            var vm = new UploadAttachmentVM
            {
                EventId = eventId,
                FileName = file.FileName,
                FilePath = uniqueFileName
            };

            // Mapping إلى Entity
            var attachment = _mapper.Map<Attachments>(vm);

            _attachmentRepo.Add(attachment);

            return (true, "File uploaded successfully");
        }
        public (bool success, string message) GetFile(int attachmentId, out string filePath, out string fileName)
        {
            var attachment = _attachmentRepo.GetById(attachmentId);

            if (attachment == null)
            {
                filePath = null;
                fileName = null;
                return (false, "File not found");
            }

            filePath = attachment.FilePath;
            fileName = attachment.FileName;

            return (true, "File ready");
        }

    }
}
