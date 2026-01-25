namespace Eventmaster.BLL.Services.Abstraction
{
    public interface IEventService
    {
        (bool success, string message, List<GetAllEventsVM> events) GetAllEvents(string? location, DateTime? date);
        (bool success, string message, GetAllEventsVM ev) GetEventById(int id);
        (bool success, string message) CreateNewEvent(CreateVM ev);
        (bool success, string message) UpdateExistingEvent(int id, UpdateVm ev);
        (bool success, string message) DeleteEvent(int id);
    }
}
