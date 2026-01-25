namespace Eventmaster.BLL.ModelVM.SavedEvent
{
    public class GetSavedEventsByParticipant
    {
        
        //string participantId,out List<Event> events
        public string ParticipantId { get; set; }
        public List<DAL.Entity.Event> Events { get; set; }
    }
}
