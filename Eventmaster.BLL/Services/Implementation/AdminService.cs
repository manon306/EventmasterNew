namespace Eventmaster.BLL.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAdminRepo _adminRepo;

        public AdminService(UserManager<User> userManager, IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
            _userManager = userManager;
        }

        public async Task<(bool success, string message)> MakeAdminAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return (false, "User not found");

            if (await _userManager.IsInRoleAsync(user, "Registered"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Registered");
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            return (true, "User promoted to Admin successfully");
        }
        public async Task<(bool success, string message, List<User> pendingOrganizers)> GetPendingOrganizersAsync()
        {
            var (success, message, pendingOrganizers) = _adminRepo.GetPendingOrganizers();
            return (success, message, pendingOrganizers);
        }
        public async Task<(bool success, string message)> ApproveOrganizerAsync(string userId)
        {
            var (success, message) =  _adminRepo.ApproveOrganizer(userId);
            return (success, message);
        }
        public async Task<(bool success, string message)> RejectOrganizerAsync(string userId)
        {
            var (success, message) = _adminRepo.RejectOrganizer(userId);
            return (success, message);
        }
        public async Task<(bool success, string message, List<Event> pendingEvents)> GetPendingEventsAsync()
        {
            var (success, message, pendingEvents) = _adminRepo.GetPendingEvents();
            return (success, message, pendingEvents);
        }
        public async Task<(bool success, string message)> AcceptEventAsync(int eventId)
        {
            var (success, message) = _adminRepo.AcceptEvent(eventId);
            return (success, message);
        }
        public async Task<(bool success, string message)> RejectEventAsync(int eventId)
        {
            var (success, message) = _adminRepo.RejectEvent(eventId);
            return (success, message);
        }
    }
}
