namespace Eventmaster.DAL.Repo.Abstraction
{
    public interface IEventRepo
    {
         (bool , string ) CreateEvent(Event newEvent);
        bool GetAllEvents(out List<Event> events, Func<Event, bool>? filter = null);
        (bool , string ) GetEventById(int eventId , out Event @event);
         (bool , string ) UpdateEvent(int eventId , Event updatedEvent);
         (bool , string ) DeleteEvent(int eventId , out Event @event);
    }
}
