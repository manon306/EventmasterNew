using Eventmaster.BLL.ModelVM.USER;

namespace Eventmaster.BLL.Services.Abstraction
{
    public interface IUserServices
    {
        (bool, string, UserProfileVm) GetUserProfile(string id);
        (bool, string, UserProfileVm) UpdateUserById(string id, UserProfileVm userDataVm);
        (bool, string, UserProfileVm) GetUserById(string id);
    }
}
