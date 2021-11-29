using Calendar.Api.Entities;
using System.Collections.Generic;

namespace Calendar.Api.Repositories
{
    public interface ICalendarEventRepository
    {    
        IEnumerable<CalendarEvent> GetAllCalendarEvents();
        void AddCalendarEvent(CalendarEvent calenderEvent);
        void DeleteCalendarEvent(CalendarEvent calendarEvent);
        IEnumerable<CalendarEvent> GetCalendarEventsForOrganizer(string eventOrganiser);
        CalendarEvent GetCalendarEvent(int id);
        IEnumerable<CalendarEvent> GetCalendarEventByLocation(string location);
        CalendarEvent GetCalendarEventByName(string name);
        IEnumerable<CalendarEvent> GetCalendarEventsSortedByTime();
        bool Save();
    }
}
