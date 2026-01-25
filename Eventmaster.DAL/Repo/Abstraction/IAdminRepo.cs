namespace Eventmaster.DAL.Repo.Abstraction
{
    public interface IAdminRepo
    {
        (bool, string, List<User>) GetPendingOrganizers();
        (bool, string) ApproveOrganizer(string userId);
        (bool, string) RejectOrganizer(string userId);
        (bool, string) AcceptEvent(int eventId);
        (bool, string) RejectEvent(int eventId);
        (bool, string, List<Event>) GetPendingEvents();
    }
}
