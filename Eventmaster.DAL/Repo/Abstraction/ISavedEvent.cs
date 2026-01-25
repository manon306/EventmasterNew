namespace Eventmaster.DAL.Repo.Abstraction
{
    public interface ISavedEvent
    {
        (bool, string) SaveEvent(int eventId, string participantId);
        (bool, string) UnsaveEvent(int eventId, string participantId);
        (bool, string) GetSavedEventsByParticipant(string participantId, out List<Event> events);
    }
}
