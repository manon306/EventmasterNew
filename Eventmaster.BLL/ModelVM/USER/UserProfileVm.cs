namespace Eventmaster.BLL.ModelVM.USER
{
    public class UserProfileVm
    {
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public status? status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ImagePath { get; set; }
        public bool IsOrganizer { get; set; }
        public bool IsApproved { get; set; } = false;
        public List<GetAllEventsVM>? Events { get; set; }
    }
}
