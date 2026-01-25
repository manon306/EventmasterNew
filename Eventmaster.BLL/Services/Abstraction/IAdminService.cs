namespace Eventmaster.BLL.Services.Abstraction
{
    public interface IAdminService
    {
        Task<(bool success, string message)> MakeAdminAsync(string userId);
        Task<(bool success, string message, List<User> pendingOrganizers)> GetPendingOrganizersAsync();
        Task<(bool success, string message)> ApproveOrganizerAsync(string userId);
        Task<(bool success, string message)> RejectOrganizerAsync(string userId);
        Task<(bool success, string message, List<Event> pendingEvents)> GetPendingEventsAsync();
        Task<(bool success, string message)> AcceptEventAsync(int eventId);
        Task<(bool success, string message)> RejectEventAsync(int eventId);
    }
}
