namespace Eventmaster.DAL.Repo.Implementation
{
    public class registerRepo : IregisterRepo
    {
        private readonly Context _db;
        public registerRepo(Context db)
        {
            _db = db;
        }

        public (bool, string) RegisterParticipant(int eventId, string participantId)
        {
            var evnt = _db.Events.FirstOrDefault(e => e.Id == eventId);
            if (evnt == null) return (false, "Event not found");
            if (evnt.NumberOfTicketsLeft <= 0) return (false, "No tickets left");

            var registration = new Registrations(participantId, eventId, "Paid", DateTime.Now);
            _db.Registrations.Add(registration);

            evnt.NumberOfTicketsLeft -= 1;
            evnt.NumberOfParticipantsSubmitted += 1;
            _db.Events.Update(evnt);

            _db.SaveChanges();
            return (true, "Registration successful");
        }
        public (bool, string, List<Registrations>) GetRegistrationsByParticipant(string participantId)
        {
            var regs = _db.Registrations.Where(r => r.ParticipantId == participantId).ToList();
            return (true, "Fetched successfully", regs);
        }
    }
}
