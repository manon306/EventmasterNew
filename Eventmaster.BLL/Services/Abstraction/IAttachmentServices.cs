using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmaster.BLL.Services.Abstraction
{
    public interface IAttachmentServices
    {
        Task<(bool success, string message)> UploadAsync(int eventId, IFormFile file);
        (bool success, string message) GetFile(int attachmentId, out string filePath, out string fileName);
    }
}
