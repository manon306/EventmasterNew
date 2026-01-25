namespace Eventmaster.DAL.Repo.Implementation
{
    public class EventRepo : IEventRepo
    {
        private readonly Context DB;

        public EventRepo(Context dB)
        {
            DB = dB;
        }

        public (bool, string) CreateEvent(Event newEvent)
        {
            if (newEvent == null)
            {
                return (false, "Event cannot be null");
            }
            DB.Events.Add(newEvent);
            DB.SaveChanges();
            return (true, "Event created successfully");

        }

        public (bool, string) DeleteEvent(int eventId, out Event e)
        {
            if (eventId <= 0)
            {
                e = null;
                return (false, "Invalid event ID");
            }
            e = DB.Events.Find(eventId);
            if (e == null)
            {
                return (false, "Event not found");
            }
            DB.Events.Remove(e);
            DB.SaveChanges();
            return (true, "Event deleted successfully");
        }

        public bool GetAllEvents(out List<Event> events, Func<Event, bool>? filter = null)
        {
            try
            {
                var query = DB.Events.AsQueryable();

                if (filter != null)
                {
                    events = query.Where(filter).ToList();
                }
                else
                {
                    events = query.ToList();
                }

                return true;
            }
            catch
            {
                events = new List<Event>();
                return false;
            }
        }

        public (bool, string) GetEventById(int eventId, out Event e)
        {
            if (eventId <= 0)
            {
                e = null;
                return (false, "Invalid event ID");
            }
            e = DB.Events.Find(eventId);
            if (e == null)
            {
                return (false, "Event not found");
            }
            return (true, "Event retrieved successfully");
        }

        public (bool, string) UpdateEvent(int eventId, Event updatedEvent)
        {
            if (eventId <= 0 || updatedEvent == null)
            {
                return (false, "Invalid input");
            }
            var exists = DB.Events.Any(x => x.Id == eventId);
            if (!exists)
            {
                return (false, "Event not found");
            }
            updatedEvent.Id = eventId;
            DB.Events.Update(updatedEvent);
            DB.SaveChanges();
            return (true, "Event updated successfully");
        }
    }
}
