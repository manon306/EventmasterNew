using Eventmaster.BLL.ModelVM.USER;
namespace Eventmaster.BLL.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public UserServices(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public (bool , string , UserProfileVm) GetUserProfile(string id)
        {
            var (success, message, user, events) = _userRepo.GetUserProfile(id);
            if (!success)
            {
                return (false, message, null);
            }
            var userProfileVm = _mapper.Map<UserProfileVm>(user);
            userProfileVm.Events = _mapper.Map<List<GetAllEventsVM>>(events);
            return (true, "User profile retrieved successfully", userProfileVm);

        }
        public (bool , string , UserProfileVm) GetUserById(string id)
        {
            var (success, message, user) = _userRepo.GetUserById(id);
            if (!success)
            {
                return (false, message, null);
            }
            var userProfileVm = _mapper.Map<UserProfileVm>(user);
            return (true, "User retrieved successfully", userProfileVm);
        }
        public (bool , string , UserProfileVm) UpdateUserById(string id, UserProfileVm userDataVm)
        {
            var userData = _mapper.Map<User>(userDataVm);
            var (success, message, user) = _userRepo.UpdateUserById(id, userData);
            if (!success)
            {
                return (false, message, null);
            }
            var userProfileVm = _mapper.Map<UserProfileVm>(user);
            return (true, "User updated successfully", userProfileVm);
        }
    }
}
