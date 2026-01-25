namespace Eventmaster.DAL.Repo.Abstraction
{
    public interface IregisterRepo
    {
        (bool, string) RegisterParticipant(int eventId, string participantId);
        (bool, string, List<Registrations>) GetRegistrationsByParticipant(string participantId);
    }
}
