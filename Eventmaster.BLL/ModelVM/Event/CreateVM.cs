namespace Eventmaster.BLL.ModelVM.Event
{
    public class CreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Eventdate { get; set; }
        public string Venue { get; set; }
        public string OrganizerId { get; set; }
    }
}
