using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;

namespace Eventmaster.BLL.Services.Implementation
{
    public class registerServices : IregisterServices
    {
        private readonly IregisterRepo _repo;
        private readonly IMapper _mapper;

        public registerServices(IregisterRepo repo,IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }
        public (bool, string) RegisterParticipant(int eventId, string participantId)
        {
            var result = _repo.RegisterParticipant(eventId, participantId);
            if (!result.Item1) return (false, result.Item2);
            return (true, result.Item2);
        }
        public (bool success, string message, List<Registrations> registrations) GetRegistrationsByParticipant(string participantId)
        {
            var result = _repo.GetRegistrationsByParticipant(participantId);
            if (!result.Item1) 
                return (false, result.Item2, new List<Registrations>());
            return (true, result.Item2, result.Item3); 
        }


    }
}
