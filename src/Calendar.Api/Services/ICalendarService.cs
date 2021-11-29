using Calendar.Api.Entities;
using Calendar.Api.Models;
using System.Collections.Generic;

namespace Calendar.Api.Services
{
    public interface ICalendarService
    {    
        IEnumerable<CalendarEventDto> GetAllCalenderEvents();
        CalendarEventDto AddCalenderEvent(CalendarEventDto calenderEvent);
        bool UpdateCalenderEvent(int id, CalendarEventDto calenderEvent);
        bool DeleteCalenderEvent(int id);
        IEnumerable<CalendarEventDto> GetCalenderEventsForOrganizer(string eventOrganiser);
        CalendarEventDto GetCalenderEvent(int id);
        IEnumerable<CalendarEventDto> GetCalenderEventByLocation(string location);
        CalendarEventDto GetCalenderEventByName(string name);
        IEnumerable<CalendarEventDto> GetCalenderEventsSortedByTime();   
    }
}
