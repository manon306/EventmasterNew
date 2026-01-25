namespace Eventmaster.DAL.Repo.Implementation
{
    public class SavedEvent : ISavedEvent
    {
        private readonly Context DB;

        public SavedEvent(Context dB)
        {
            DB = dB;
        }
        public (bool, string) GetSavedEventsByParticipant(string participantId, out List<Event> events)
        {
            if(string.IsNullOrEmpty(participantId))
            {
                events = new List<Event>();
                return (false, "Participant ID cannot be null or empty.");
            }
            events = DB.SavedEvents.Where(se => se.ParticipantId == participantId).Select(e=>e.Event).ToList();
            return (true, "Saved events retrieved successfully.");
        }

        public (bool, string) SaveEvent(int eventId, string participantId)
        {
            if (string.IsNullOrEmpty(participantId))
            {
                return (false, "Participant ID cannot be null or empty.");
            }
            if (!DB.Events.Any(e => e.Id == eventId))
            {
                return (false, "Event does not exist.");
            }
            DB.SavedEvents.Add(new SavedEvents
            {
                EventId = eventId,
                ParticipantId = participantId
            });
            DB.SaveChanges();
            return (true, "Event saved successfully.");
        }

        public (bool, string) UnsaveEvent(int eventId, string participantId)
        {
            if (string.IsNullOrEmpty(participantId))
            {
                return (false, "Participant ID cannot be null or empty.");
            }
            if (!DB.Events.Any(e => e.Id == eventId))
            {
                return (false, "Event does not exist.");
            }
            var savedEvent = DB.SavedEvents.FirstOrDefault(se => se.EventId == eventId && se.ParticipantId == participantId);
            if (savedEvent == null)
            {
                return (false, "Saved event not found.");
            }
            DB.SavedEvents.Remove(savedEvent);
            DB.SaveChanges();
            return (true, "Event unsaved successfully.");
        }
    }
}
