
using Calendar.Api.DbContexts;
using Calendar.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Api.Repositories
{
    public class CalendarEventRepository : ICalendarEventRepository
    {
        private readonly CalendarEventContext _context;

        public CalendarEventRepository(CalendarEventContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<CalendarEvent> GetAllCalendarEvents()
        {
            return _context.CalendarEvents.ToList();
        }

        public void AddCalendarEvent(CalendarEvent calenderEvent)
        {

            _context.CalendarEvents.Add(calenderEvent);
        }

        public void DeleteCalendarEvent(CalendarEvent calendarEvent)
        {
            

           _context.CalendarEvents.Remove(calendarEvent);
        }

        public IEnumerable<CalendarEvent> GetCalendarEventsForOrganizer(string eventOrganiser)
        {
           

            return _context.CalendarEvents.Where(e => e.EventOrganizer == eventOrganiser).ToList();
        }

        public CalendarEvent GetCalendarEvent(int id)
        {
            return _context.CalendarEvents.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<CalendarEvent> GetCalendarEventByLocation(string location)
        {
           
            return _context.CalendarEvents.Where(e=>e.Location==location).ToList();
        }

        public CalendarEvent GetCalendarEventByName(string name)
        {
            

            return _context.CalendarEvents.FirstOrDefault(e=>e.Name==name);
        }

        public IEnumerable<CalendarEvent> GetCalendarEventsSortedByTime()
        {
            return _context.CalendarEvents.OrderByDescending(e => e.Time);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
