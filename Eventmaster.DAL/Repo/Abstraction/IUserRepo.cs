namespace Eventmaster.DAL.Repo.Abstraction
{
    public interface IUserRepo
    {
        (bool success, string message, User user, List<Event> events) GetUserProfile(string id);
        (bool success, string message, User user) UpdateUserById(string id, User userData);
        (bool, string, User) GetUserById(string id);
    }

}
