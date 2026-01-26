
namespace Eventmaster.DAL.Repo.Implementation
{
    public class UserRepo :IUserRepo
    {
        private readonly Context DB;
        public UserRepo(Context context)
        {
            DB = context;
        }
        public (bool success, string message, User user, List<Event> events) GetUserProfile(string id)
        {
            var user = DB.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return (false, "User not found", null, null);
            }

            var eventsRelatedToThisUser = DB.Events
                                           .Where(e => e.OrganizerId == id)
                                           .ToList();

            return (true, "User found", user, eventsRelatedToThisUser);
        }
        public (bool , string , User) GetUserById(string id)
        {
            var user = DB.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return (false, "User not found", null);
            }
            return (true, "User found", user);
        }
        public (bool success, string message, User user) UpdateUserById(string id, User userData)
        {
            var user = DB.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return (false, "User not found", null);
            }
            var updatedUser = new User(
                userData.Name,
                userData.Bio,
                userData.status,
                userData.IsOrganizer,
                userData.IsApproved,
                userData.ImagePath
            );

            user.Name = updatedUser.Name;
            user.Bio = updatedUser.Bio;
            user.status = updatedUser.status;
            user.IsOrganizer = updatedUser.IsOrganizer;
            user.IsApproved = updatedUser.IsApproved;
            user.ImagePath = updatedUser.ImagePath;
            DB.SaveChanges();
            return (true, "User updated successfully", user);
        }

    }
}
