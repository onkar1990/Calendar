
using AutoMapper;
using Calendar.Api.Entities;
using Calendar.Api.Models;
using Calendar.Api.Repositories;
using System;
using System.Collections.Generic;

namespace Calendar.Api.Services
{
    public class CalendarService : ICalendarService
    {

        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly IMapper _mapper;

        public CalendarService(ICalendarEventRepository calendarEventRepository, IMapper mapper)
        {

            _calendarEventRepository = calendarEventRepository ?? throw new ArgumentNullException(nameof(calendarEventRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CalendarEventDto GetCalenderEvent(int id)
        {

            var calendarEventFromRepo = _calendarEventRepository.GetCalendarEvent(id);
            if (calendarEventFromRepo == null)
            {
                return null;
            }
            return _mapper.Map<CalendarEventDto>(calendarEventFromRepo);
        }

        public CalendarEventDto AddCalenderEvent(CalendarEventDto calenderEvent)
        {

            var calendarEventFromRepo = _mapper.Map<CalendarEvent>(calenderEvent);

            _calendarEventRepository.AddCalendarEvent(calendarEventFromRepo);
            _calendarEventRepository.Save();

            return _mapper.Map<CalendarEventDto>(calendarEventFromRepo);


        }

        public bool UpdateCalenderEvent(int id, CalendarEventDto calenderEvent)
        {
            var calendarEventFromRepo = _calendarEventRepository.GetCalendarEvent(id);

            if (calendarEventFromRepo == null)
            {
                return false;
            }

            _mapper.Map(calenderEvent, calendarEventFromRepo);
            _calendarEventRepository.Save();
            return true;
        }

        public bool DeleteCalenderEvent(int id)
        {
            var calendarEventFromRepo = _calendarEventRepository.GetCalendarEvent(id);

            if (calendarEventFromRepo == null)
            {
                return false;
            }

            _calendarEventRepository.DeleteCalendarEvent(calendarEventFromRepo);
            _calendarEventRepository.Save();
            return true;


        }

        public IEnumerable<CalendarEventDto> GetAllCalenderEvents()
        {
            var calendarEventsFromRepo = _calendarEventRepository.GetAllCalendarEvents();
            return _mapper.Map<IEnumerable<CalendarEventDto>>(calendarEventsFromRepo);
        }

        public IEnumerable<CalendarEventDto> GetCalenderEventsForOrganizer(string eventOrganiser)
        {
            var calendarEventsFromRepo = _calendarEventRepository.GetCalendarEventsForOrganizer(eventOrganiser);
            return _mapper.Map<IEnumerable<CalendarEventDto>>(calendarEventsFromRepo);
        }

        public IEnumerable<CalendarEventDto> GetCalenderEventByLocation(string location)
        {
            var calendarEventsFromRepo = _calendarEventRepository.GetCalendarEventByLocation(location);
            return _mapper.Map<IEnumerable<CalendarEventDto>>(calendarEventsFromRepo);
        }

        public CalendarEventDto GetCalenderEventByName(string name)
        {
            var calendarEventsFromRepo = _calendarEventRepository.GetCalendarEventByName(name);
            return _mapper.Map<CalendarEventDto>(calendarEventsFromRepo);
        }

        public IEnumerable<CalendarEventDto> GetCalenderEventsSortedByTime()
        {
            var calendarEventsFromRepo = _calendarEventRepository.GetCalendarEventsSortedByTime();
            return _mapper.Map<IEnumerable<CalendarEventDto>>(calendarEventsFromRepo);
        }
    }
}
