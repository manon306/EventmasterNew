namespace Eventmaster.DAL.Entity
{
    public class Event
    {
        /*
         * Event includes: Event organizer name, Title, Description, Venue, Event date, Ticket
            Price, Number of tickets left, and Number of participants submitted.
         */
        public int Id { get; set; }
        public string? OrganizerName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public DateTime Eventdate { get; set; }
        public int TicketPrice { get; set; }
        public int NumberOfTicketsLeft { get; set; }
        public int NumberOfParticipantsSubmitted { get; set; }
        public status Status { get; set; }

        public string OrganizerId { get; set; }
        public User Organizer { get; set; }
        public List<Registrations> Registration { get; set; }

        public Event() { }
        public Event(string? organizerName, string title, string description, string venue, DateTime eventdate, int ticketPrice, int numberOfTicketsLeft, int numberOfParticipantsSubmitted, string organizerId)
        {
            OrganizerName = organizerName;
            Title = title;
            Description = description;
            Venue = venue;
            Eventdate = eventdate;
            TicketPrice = ticketPrice;
            NumberOfTicketsLeft = numberOfTicketsLeft;
            NumberOfParticipantsSubmitted = numberOfParticipantsSubmitted;
            OrganizerId = organizerId;
        }
    }
}
