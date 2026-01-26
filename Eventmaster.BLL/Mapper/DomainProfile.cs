using AutoMapper;
using Eventmaster.BLL.ModelVM.Event;
using Eventmaster.BLL.ModelVM.SavedEvent;
using Eventmaster.BLL.ModelVM.USER;
using Eventmaster.DAL.Entity;



namespace Eventmaster.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // CreateMap<Source, Destination>();
            #region Event Mappings
                CreateMap<Event, CreateVM>().ReverseMap();
                CreateMap<Event, GetAllEventsVM>().ReverseMap();
                CreateMap<Event, UpdateVm>().ReverseMap();
            #endregion
            #region Saved Event Mappings
                CreateMap<SavedEvents, GetSavedEventsByParticipant>().ReverseMap();
            #endregion
            #region Attachment Mappings
                CreateMap<Attachments, Eventmaster.BLL.ModelVM.Attachment.UploadAttachmentVM>().ReverseMap();
            #endregion
            #region User
            // من User → UserProfileVm
            CreateMap<User, UserProfileVm>();

            // من UserProfileVm → User (التحديث)
            CreateMap<UserProfileVm, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // مهم جدًا

            #endregion
            
        }
    }
}
