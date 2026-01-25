namespace Eventmaster.DAL.Entity
{
    public class Registrations
    {
        //reg_id (PK), participant_id (FK), event_id (FK), payment_status, booking_date
        [Key]
        public int RegId { get; set; }
        public string ParticipantId { get; set; }

        public int EventId { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime BookingDate { get; set; }
        public User Participant { get; set; }
        public Event Event { get; set; }
        public Registrations() { }
        public Registrations(string participantId, int eventId, string paymentStatus, DateTime bookingDate)
        {
            ParticipantId = participantId;
            EventId = eventId;
            PaymentStatus = paymentStatus;
            BookingDate = bookingDate;
        }
    }
}
