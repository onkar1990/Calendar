using AutoFixture;
using Calendar.Api.Controllers;
using Calendar.Api.Models;
using Calendar.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Calendar.Api.Tests
{
    public class CalendarControllerTests
    {

        private readonly Mock<ICalendarService> _calendarServiceMock = new();
        private ICalendarService CalendarService => _calendarServiceMock.Object;
        private static Fixture Fixture => new();

        [Fact]
        public void Constructor_WhenArgumentIsNull_ThrowsException()
        {
            //Arrange
            var exception = Assert.Throws<ArgumentNullException>(() => new CalendarController(null));

            //Assert
            exception.Should().NotBeNull();
            exception.ParamName.Should().Be("calendarService");
        }

        [Fact]
        public void Get_WhenThereAreEvents_ShouldReturnActionResultOfAllEventsWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
            var expectedCalendarEvents = Fixture.CreateMany<CalendarEventDto>(3).ToList();
            _calendarServiceMock.Setup(f => f.GetAllCalenderEvents()).Returns(expectedCalendarEvents);

            //Act
             var response = sut.Get();
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<List<CalendarEventDto>>();
            var actualCalendarEvents = result.Value as List<CalendarEventDto>;
            actualCalendarEvents.Should().NotBeNull();
            actualCalendarEvents.Count.Should().Be(3);
        }

        [Fact]
        public void Get_WhenThereAreNoEvent_ShouldReturnEmptyActionResultWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
          
            _calendarServiceMock.Setup(f => f.GetAllCalenderEvents()).Returns(new List<CalendarEventDto>());

            //Act
            var response = sut.Get();
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<List<CalendarEventDto>>();
            var actualCalendarEvents = result.Value as List<CalendarEventDto>;
            actualCalendarEvents.Should().NotBeNull();
            actualCalendarEvents.Count.Should().Be(0);
        }


        [Fact]
        public void Post_WhenThereIsEvent_ShouldReturnCreatedAtRouteResponseWithCreatedEventAnd201StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
            var expectedCalendarEvent = Fixture.Create<CalendarEventDto>();
            _calendarServiceMock.Setup(f => f.AddCalenderEvent(expectedCalendarEvent)).Returns(expectedCalendarEvent);
           
           
            //Act
            var response = sut.Post(expectedCalendarEvent);
            var result = response.Result as CreatedAtRouteResult;

            //Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<CalendarEventDto>();
            result.RouteName.Should().Be("query");
            result.RouteValues["id"].Should().Be(expectedCalendarEvent.Id);

            var actualCalendarEvents = result.Value as CalendarEventDto;
            actualCalendarEvents.Should().NotBeNull();
            actualCalendarEvents.Should().Be(expectedCalendarEvent);
            
         
        }

        [Fact]
        public void Put_WhenDifferentEventId_ShouldReturnBadRequestResponseWith400StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);

            //Act
            var response = sut.Put(Fixture.Create<int>(), Fixture.Create<CalendarEventDto>());

            //Assert
            response.Should().BeOfType<BadRequestResult>();

        }

        [Fact]
        public void Put_WhenEventDoesnotExists_ShouldReturnNotFoundResponseWith404StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
            var expectedEventId = Fixture.Create<int>();
            var expectedCalendarEvent = Fixture.Create<CalendarEventDto>();
            expectedCalendarEvent.Id = expectedEventId;
            _calendarServiceMock.Setup(f => f.UpdateCalenderEvent(It.IsAny<int>(), It.IsAny<CalendarEventDto>())).Returns(false);

            //Act
            var response = sut.Put(expectedEventId, expectedCalendarEvent);

            //Assert
            response.Should().BeOfType<NotFoundResult>();

        }

        [Fact]
        public void Put_WhenEventIdIsSameAndExists_ShouldReturnOkResponseWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
            var expectedEventId = Fixture.Create<int>();
            var expectedCalendarEvent = Fixture.Create<CalendarEventDto>();
            expectedCalendarEvent.Id = expectedEventId;
            _calendarServiceMock.Setup(f => f.UpdateCalenderEvent(It.IsAny<int>(), It.IsAny<CalendarEventDto>())).Returns(true);

            //Act
            var response = sut.Put(expectedEventId, expectedCalendarEvent);

            //Assert
            response.Should().BeOfType<OkResult>();


        }

        [Fact]
        public void Delete_WhenEventIdExists_ShouldReturnOkResponseWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
          
            _calendarServiceMock.Setup(f => f.DeleteCalenderEvent(It.IsAny<int>())).Returns(true);

            //Act
            var response = sut.DeleteCalenderEvent(Fixture.Create<int>());

            //Assert
            response.Should().BeOfType<OkResult>();


        }

        [Fact]
        public void Delete_WhenEventIdDoesNotExists_ShouldReturnNotFoundResponseWith404StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);

            _calendarServiceMock.Setup(f => f.DeleteCalenderEvent(It.IsAny<int>())).Returns(false);

            //Act
            var response = sut.DeleteCalenderEvent(Fixture.Create<int>());

            //Assert
            response.Should().BeOfType<NotFoundResult>();


        }

        [Fact]
        public void Get_WhenEventExistsWithFilterId_ShouldReturnActionResultOfEventWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
            var expectedCalendarEvent = Fixture.Create<CalendarEventDto>();
            _calendarServiceMock.Setup(f => f.GetCalenderEvent(expectedCalendarEvent.Id)).Returns(expectedCalendarEvent);

            //Act
            var response = sut.GetCalendarEventBy(expectedCalendarEvent.Id);
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<CalendarEventDto>();
            var actualCalendarEvent = result.Value as CalendarEventDto;
            actualCalendarEvent.Should().NotBeNull();
            actualCalendarEvent.Should().Be(expectedCalendarEvent);
        }

        [Fact]
        public void Get_WhenEventsExistsWithFilterName_ShouldReturnActionResultOfEventsWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);

            var expectedCalendarEvent = Fixture.Create<CalendarEventDto>();

            _calendarServiceMock.Setup(f => f.GetCalenderEventByName(expectedCalendarEvent.Name)).Returns(expectedCalendarEvent);

            //Act
            var response = sut.GetCalendarEventBy(name: expectedCalendarEvent.Name);
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<CalendarEventDto>();
            var actualCalendarEvent = result.Value as CalendarEventDto;
            actualCalendarEvent.Should().NotBeNull() 
                                        .And.Be(expectedCalendarEvent);
        }

        [Fact]
        public void Get_WhenEventsExistsWithFilterEventOrganizer_ShouldReturnActionResultOfEventsWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
            var expectedEventOrganizerName = Fixture.Create<string>();
            var expectedCalendarEvents = Fixture.CreateMany<CalendarEventDto>(5).ToList();
            expectedCalendarEvents.ForEach(f => f.EventOrganizer = expectedEventOrganizerName);
        
            

            _calendarServiceMock.Setup(f => f.GetCalenderEventsForOrganizer(expectedEventOrganizerName)).Returns(expectedCalendarEvents);

            //Act
            var response = sut.GetCalendarEventBy(eventOrganizer: expectedEventOrganizerName);
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<List<CalendarEventDto>>();
            var actualCalendarEvent = result.Value as List<CalendarEventDto>;
            actualCalendarEvent.Should().NotBeNull()
                                        .And .Equal(expectedCalendarEvents, (c1, c2) => c1.EventOrganizer == c2.EventOrganizer);
        }

        [Fact]
        public void Get_WhenEventsExistsWithFilterLocation_ShouldReturnActionResultOfEventsWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
            var expectedLocation = Fixture.Create<string>();
            var expectedCalendarEvents = Fixture.CreateMany<CalendarEventDto>(5).ToList();
            expectedCalendarEvents.ForEach(f => f.Location = expectedLocation);



            _calendarServiceMock.Setup(f => f.GetCalenderEventByLocation(expectedLocation)).Returns(expectedCalendarEvents);

            //Act
            var response = sut.GetCalendarEventBy(location: expectedLocation);
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<List<CalendarEventDto>>();
            var actualCalendarEvent = result.Value as List<CalendarEventDto>;
            actualCalendarEvent.Should().NotBeNull() 
                                        .And .Equal(expectedCalendarEvents, (c1, c2) => c1.Location == c2.Location);
        }

        [Fact]
        public void Get_WhenAllEventsStortedByTime_ShouldReturnActionResultOfEventsWith200StatusCode()
        {
            //Arrange
            var sut = new CalendarController(CalendarService);
            var expectedCalendarEvents = Fixture.CreateMany<CalendarEventDto>(5).OrderByDescending(f=>f.Time).ToList();
            _calendarServiceMock.Setup(f => f.GetCalenderEventsSortedByTime()).Returns(expectedCalendarEvents);

            //Act
            var response = sut.GetAllSorted();
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<List<CalendarEventDto>>();
            var actualCalendarEvent = result.Value as List<CalendarEventDto>;

            actualCalendarEvent.Should().NotBeNull() 
                                        .And .BeEquivalentTo(expectedCalendarEvents)
                                        .And .BeInDescendingOrder(x => x.Time);
        }
    }


}
