namespace Eventmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedEventController : ControllerBase
    {
        private readonly ISavedEventServices services;
        public SavedEventController(ISavedEventServices services)
        {
            this.services = services;
        }
        //api/SavedEvent/SaveEvent/{eventId}
        [Authorize(Roles = "Registered")]
        [HttpPost("SaveEvent/{eventId}")]
        public IActionResult SaveEvent(int eventId)
        {
            var participantId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(participantId))
                return Unauthorized("Invalid token: UserId not found");

            var result = services.SaveEvent(eventId, participantId);

            if (!result.Item1)
                return BadRequest(result.Item2);

            return Ok(result.Item2);
        }
        //api/SavedEvent/UnsaveEvent/{eventId}
        [Authorize(Roles= "Registered")]
        [HttpDelete("UnsaveEvent/{eventId}")]
        public IActionResult UnsaveEvent(int eventId)
        {
            var participantId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(participantId))
                return Unauthorized("Invalid token: UserId not found");
            var result = services.UnsaveEvent(eventId, participantId);
            if (!result.Item1)
                return BadRequest(result.Item2);
            return Ok(result.Item2);
        }
        //api/SavedEvent/GetSavedEvents
        [Authorize(Roles = "Registered")]
        [HttpGet("GetSavedEvents")]
        public IActionResult GetSavedEvents()
        {
            var participantId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(participantId))
                return Unauthorized("Invalid token: UserId not found");
            var result = services.GetSavedEventsByParticipant(participantId, out List<GetSavedEventsByParticipant> events);
            if (!result.Item1)
                return BadRequest(result.Item2);
            return Ok(events);
        }


    }
}
