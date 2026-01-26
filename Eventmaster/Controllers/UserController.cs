using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eventmaster.BLL.ModelVM.USER;
namespace Eventmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("profile/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserProfile(string id)
        {
            var (success, message, userProfileVm) = _userServices.GetUserProfile(id);
            if (!success)
            {
                return NotFound(new { message });
            }
            return Ok(new { message, userProfileVm });
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProfile(string id)
        {
            var result = _userServices.GetUserById(id);
            if (!result.Item1)
            {
                return NotFound(new { message = result.Item2 });
            }
            return Ok(new { message = result.Item2, user = result.Item3 });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUserById(string id, [FromForm] UserProfileVm userDataVm, IFormFile profileImage)
        {
            if (profileImage != null)
            {
                // تأكد من وجود المجلد
                var folder = Path.Combine("wwwroot/images/profiles");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = $"{Guid.NewGuid()}_{profileImage.FileName}";
                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    profileImage.CopyTo(stream);
                }

                // تحديث الرابط في الـ VM
                userDataVm.ImagePath = $"/images/profiles/{fileName}";
            }

            var (success, message, updatedUserVm) = _userServices.UpdateUserById(id, userDataVm);

            if (!success)
                return NotFound(new { message });

            return Ok(new { message, updatedUserVm });
        }


    }
}
