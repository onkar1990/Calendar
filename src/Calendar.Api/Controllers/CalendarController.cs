using Calendar.Api.Models;
using Calendar.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace Calendar.Api.Controllers
{

    [ApiController]
    [Route("api/calendar")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;


        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException(nameof(calendarService));

        }

        /// <summary>
        /// Add a new event to the Calendar.
        /// </summary>
        /// <param name="calendarEvent"></param>
        /// <response code="201">Event created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<CalendarEventDto> Post(CalendarEventDto calendarEvent)
        {

            var calenderEventToReturn = _calendarService.AddCalenderEvent(calendarEvent);

            return CreatedAtAction("Post", calenderEventToReturn);

        }

        /// <summary>
        /// Edit an event from the calendar.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="calendarEvent"></param>
        /// <response code="200">Event was updated successfully.</response>
        /// <response code="400">Event could not be added due to Id mismatch.</response>
        /// <response code="404">Event could not be found with the given Id.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(int id, CalendarEventDto calendarEvent)
        {

            if (id != calendarEvent.Id)
            {

                return BadRequest();
            }

            var isSuccess = _calendarService.UpdateCalenderEvent(id, calendarEvent);

            if (isSuccess)
            {
                return Ok();
            }

            return NotFound();

        }

        /// <summary>
        /// Delete an event from the calendar with given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Event deleted.</response>
        /// <response code="404">Event could not be found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteCalenderEvent(int id)
        {


            var isSuccess = _calendarService.DeleteCalenderEvent(id);

            if (isSuccess)
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Get all events from the Calendar.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">All events were retrieved.</response>
        [HttpGet]
        public ActionResult<IEnumerable<CalendarEventDto>> Get()
        {
            var calenderEvents = _calendarService.GetAllCalenderEvents();

            return Ok(calenderEvents);
        }

        /// <summary>
        /// Get all events from the calendar with given matching filter.
        /// </summary>
        /// <param name="id"></param>param>
        /// <param name="name"></param>
        /// <param name="eventOrganizer"></param>
        /// <param name="location"></param>
        /// <remarks>Events will be retrieved according to only one filter parameter.</remarks>
        /// <response code="200">Event where retreived. </response>
        /// <response code="204">The matching filter is not correct.</response>
        [HttpGet]
        [Route("query")]
        public ActionResult<IEnumerable<CalendarEventDto>> GetCalendarEventBy(int? id = null, string? name = null, string? eventOrganizer = null, string? location = null)
        {

            if (id != null)
            {
           
                return Ok(_calendarService.GetCalenderEvent(id.Value));
            }

            else if (name != null)
            {

                return Ok(_calendarService.GetCalenderEventByName(name));

            }

            else if (eventOrganizer != null)
            {
                return Ok(_calendarService.GetCalenderEventsForOrganizer(eventOrganizer));
            }

            else if (location != null)
            {
                return Ok(_calendarService.GetCalenderEventByLocation(location));
            }

            return NoContent();

        }

        /// <summary>
        /// Get all events sorted by time (desc order) from the Calendar.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">All events were fetched and are ordered by time.</response>
        [HttpGet]
        [Route("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CalendarEventDto>> GetAllSorted()
        {
            return Ok(_calendarService.GetCalenderEventsSortedByTime());
        }
    }
}
