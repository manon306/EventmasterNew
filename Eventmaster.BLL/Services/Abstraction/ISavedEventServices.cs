namespace Eventmaster.BLL.Services.Abstraction
{
    public interface ISavedEventServices
    {
        (bool, string) GetSavedEventsByParticipant(string participantId, out List<GetSavedEventsByParticipant> events);
        (bool, string) SaveEvent(int eventId, string participantId);
        (bool, string) UnsaveEvent(int eventId, string participantId);
    }
}
