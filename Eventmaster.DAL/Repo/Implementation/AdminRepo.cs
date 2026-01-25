
namespace Eventmaster.DAL.Repo.Implementation
{
    public class AdminRepo : IAdminRepo
    {
        private readonly Context _db;

        public AdminRepo(Context db)
        {
            _db = db;
        }

        public (bool, string, List<User>) GetPendingOrganizers()
        {
            var pendingUsers = _db.Users
                .Where(u => u.IsOrganizer && !u.IsApproved)
                .ToList();

            if (!pendingUsers.Any())
                return (false, "No pending organizers found.", new List<User>());

            return (true, "Pending organizers retrieved successfully.", pendingUsers);
        }

        public (bool, string) ApproveOrganizer(string userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId && u.IsOrganizer && !u.IsApproved);
            if (user == null)
                return (false, "User not found or already approved.");

            user.IsApproved = true;
            _db.SaveChanges();
            return (true, "Organizer approved successfully.");
        }

        public (bool, string) RejectOrganizer(string userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId && u.IsOrganizer && !u.IsApproved);
            if (user == null)
                return (false, "User not found or already processed.");

            _db.Users.Remove(user);
            _db.SaveChanges();
            return (true, "Organizer rejected successfully.");
        }
        public (bool, string) AcceptEvent(int eventId)
        {
            var evnt = _db.Events.FirstOrDefault(e => e.Id == eventId && e.Status==status.Pending);
            if (evnt == null)
                return (false, "Event not found or already approved.");
            evnt.Status = status.Approved;
            _db.SaveChanges();
            return (true, "Event approved successfully.");
        }
        public (bool, string) RejectEvent(int eventId)
        {
            var evnt = _db.Events.FirstOrDefault(e => e.Id == eventId && e.Status==status.Pending);
            if (evnt == null)
                return (false, "Event not found or already processed.");
            _db.Events.Remove(evnt);
            _db.SaveChanges();
            return (true, "Event rejected successfully.");
        }
        public (bool, string, List<Event>) GetPendingEvents()
        {
            var pendingEvents = _db.Events
                .Where(e => e.Status == status.Pending)
                .ToList();
            if (!pendingEvents.Any())
                return (false, "No pending events found.", new List<Event>());
            return (true, "Pending events retrieved successfully.", pendingEvents);
        }
    }

}
