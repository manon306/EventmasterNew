namespace Eventmaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] string? location, [FromQuery] DateTime? date)
        {
            var result = _eventService.GetAllEvents(location, date);

            if (!result.success) return BadRequest(result.message);
            return Ok(result.events);
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "Admin,Registered")]
        public IActionResult GetById(int id)
        {
            var result = _eventService.GetEventById(id);
            if (!result.success) return NotFound(result.message);
            return Ok(result.ev);
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Create(CreateVM newEvent)
        {
            var result = _eventService.CreateNewEvent(newEvent);
            if (!result.success) return BadRequest(result.message);
            return Ok(result.message);
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Update(int id, UpdateVm updatedEvent)
        {

            var result = _eventService.UpdateExistingEvent(id, updatedEvent);
            if (!result.success) return BadRequest(result.message);
            return Ok(result.message);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Delete(int id)
        {
            var result = _eventService.DeleteEvent(id);
            if (!result.success) return BadRequest(result.message);
            return Ok(result.message);
        }
    }
}