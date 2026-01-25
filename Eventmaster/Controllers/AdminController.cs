using Eventmaster.API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Eventmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public AdminController(IAdminService adminService,IHubContext<NotificationHub> hubContext)
        {
            _adminService = adminService;
            _hubContext = hubContext;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("MakeAdmin/{userId}")]
        public async Task<IActionResult> MakeAdmin(string userId)
        {
            var result = await _adminService.MakeAdminAsync(userId);

            if (!result.success)
                return BadRequest(result.message);

            return Ok(result.message);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("PendingOrganizers")]
        public async Task<IActionResult> GetPendingOrganizers()
        {
            var result = await _adminService.GetPendingOrganizersAsync();
            if (!result.success)
                return BadRequest(result.message);
            return Ok(result.pendingOrganizers);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("ApproveOrganizer/{userId}")]
        public async Task<IActionResult> ApproveOrganizer(string userId)
        {
            var result = await _adminService.ApproveOrganizerAsync(userId);
            if (!result.success)
                return BadRequest(result.message);
            return Ok(result.message);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("RejectOrganizer/{userId}")]
        public async Task<IActionResult> RejectOrganizer(string userId)
        {
            var result = await _adminService.RejectOrganizerAsync(userId);
            if (!result.success)
                return BadRequest(result.message);
            return Ok(result.message);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("PendingEvents")]
        public async Task<IActionResult> GetPendingEvents()
        {
            var result = await _adminService.GetPendingEventsAsync();
            if (!result.success)
                return BadRequest(result.message);
            return Ok(result.pendingEvents);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AcceptEvent/{eventId}")]
        public async Task<IActionResult> AcceptEvent(int eventId)
        {
            var result = await _adminService.AcceptEventAsync(eventId);
            if (!result.success)
                return BadRequest(result.message);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification",
            "Admin", $"New Event has been approved! Check it out.");
            return Ok(result.message);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("RejectEvent/{eventId}")]
        public async Task<IActionResult> RejectEvent(int eventId)
        {
            var result = await _adminService.RejectEventAsync(eventId);
            if (!result.success)
                return BadRequest(result.message);
            return Ok(result.message);
        }
    }
}
