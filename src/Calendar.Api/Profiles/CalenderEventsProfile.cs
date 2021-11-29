using AutoMapper;


namespace Calendar.Api.Profiles
{
    public class CalenderEventsProfile :Profile
    {
        public CalenderEventsProfile()
        {
            CreateMap<Entities.CalendarEvent, Models.CalendarEventDto>();
            CreateMap<Models.CalendarEventDto,Entities.CalendarEvent>();

        }

    }
}
