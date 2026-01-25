namespace Eventmaster.BLL.Services.Implementation
{
    public class SavedEventServices : ISavedEventServices
    {
        private readonly ISavedEvent eventRepo;
        private readonly IMapper mapper;
        public SavedEventServices(ISavedEvent eventRepo, IMapper mapper)
        {
            this.eventRepo = eventRepo;
            this.mapper = mapper;
        }
        public (bool, string) GetSavedEventsByParticipant(string participantId, out List<GetSavedEventsByParticipant> events)
        {
            var result = eventRepo.GetSavedEventsByParticipant(participantId, out List<DAL.Entity.Event> savedEvents);
            var eventsVM = mapper.Map<List<GetSavedEventsByParticipant>>(savedEvents);
            events = eventsVM;
            return (result.Item1, result.Item2);
        }

        public (bool, string) SaveEvent(int eventId, string participantId)
        {
            var result = eventRepo.SaveEvent(eventId, participantId);
            return (result.Item1, result.Item2);
        }

        public (bool, string) UnsaveEvent(int eventId, string participantId)
        {
            var result = eventRepo.UnsaveEvent(eventId, participantId);
            return (result.Item1, result.Item2);
        }
    }
}
