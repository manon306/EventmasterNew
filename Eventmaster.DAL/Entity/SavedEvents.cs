namespace Eventmaster.DAL.Entity
{
    public class SavedEvents
    {
        //save_id(PK), participant_id(FK), event_id(FK)
        [Key]
        public int SaveId { get; set; }
        public string ParticipantId { get; set; }
        public int EventId { get; set; }
        public User Participant { get; set; }
        public Event Event { get; set; }

        public SavedEvents() { }
        public SavedEvents(string participantId, int eventId)
        {
            ParticipantId = participantId;
            EventId = eventId;
        }

    }
}
