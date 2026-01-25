namespace Eventmaster.BLL.Services.Abstraction
{
    public interface IregisterServices
    {
        (bool, string) RegisterParticipant(int eventId, string participantId);
        (bool success, string message, List<Registrations> registrations) GetRegistrationsByParticipant(string participantId);

    }
}
