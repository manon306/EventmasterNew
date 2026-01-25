namespace Eventmaster.BLL.Services.Implementation
{
    public class EventService : IEventService
    {
        private readonly IEventRepo _eventRepo;
        private readonly IMapper mapper;

        public EventService(IEventRepo eventRepo , IMapper mapper)
        {
            _eventRepo = eventRepo;
            this.mapper = mapper;
        }

        public (bool success, string message, List<GetAllEventsVM> events) GetAllEvents(string? location, DateTime? date)
        {
            Func<Event, bool> filter = e =>
                (string.IsNullOrEmpty(location) || e.Venue.Contains(location)) &&
                (!date.HasValue || e.Eventdate.Date == date.Value.Date);

            var result = _eventRepo.GetAllEvents(out List<Event> events, filter);

            var eventsVM = mapper.Map<List<GetAllEventsVM>>(events);
            return (result, result ? "Success" : "Error", eventsVM);
        }

        public (bool success, string message, GetAllEventsVM ev) GetEventById(int id)
        {
            var result = _eventRepo.GetEventById(id, out Event ev);
            var evVM = mapper.Map<GetAllEventsVM>(ev);
            return (result.Item1, result.Item2, evVM);
        }

        public (bool success, string message) CreateNewEvent(CreateVM ev)
        {
            if (ev.Eventdate < DateTime.Now)
            {
                return (false, "Invalid Date");
            }
            var eventEntity = mapper.Map<Event>(ev);
            return _eventRepo.CreateEvent(eventEntity);
        }

        public (bool success, string message) UpdateExistingEvent(int id, UpdateVm ev)
        {

            var eventEntity = mapper.Map<Event>(ev);
            return _eventRepo.UpdateEvent(id, eventEntity);
        }

        public (bool success, string message) DeleteEvent(int id)
        {
            return _eventRepo.DeleteEvent(id, out _);
        }

    }
}
