using Eventmaster.API.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Eventmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class registerController : ControllerBase
    {
        private readonly IregisterServices registerServices;
        private readonly IHubContext<NotificationHub> _hubContext;
        public registerController(IregisterServices registerServices, IHubContext<NotificationHub> hubContext)
        {
            this.registerServices = registerServices;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetRegistrationsByParticipant(string participantId)
        {

            var result = registerServices.GetRegistrationsByParticipant(participantId);
            if(!result.success) return BadRequest(result.message);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterParticipant(int eventId)
        {
            var participantId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(participantId))
                return Unauthorized("User not found in token");

            var result = registerServices.RegisterParticipant(eventId, participantId);
            if (result.Item1)
            {
                _hubContext.Clients.All.SendAsync("ReceiveNotification", "System", "A new participant has joined the event!");
            }
            if (!result.Item1) return BadRequest(result.Item2);
            return Ok(result.Item2);
        }
        [Authorize(Roles = "Organizer,Admin")]
        [HttpPost("SendEventUpdate/{eventId}")]
        public async Task<IActionResult> SendUpdate(int eventId, [FromBody] string updateMessage)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification",
                $"Update for Event #{eventId}",
                updateMessage);

            return Ok("Update message sent successfully!");
        }
    }
}
