using System.Linq;
using System.Collections.Generic;
using System.Data;
using TI_BackEnd.Domain.Event;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.EventDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using Xunit;

namespace unit_tests
{
    public class EventTests
    {
        private EventRepository _eventRepository = new EventRepository();
        private PlanningRepository _planningRepository = new PlanningRepository();
        private Planning _planningInput = new Planning(0, "label", 0);

        [Fact]
        public void Create()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Event eventInput = new Event(0, 1, planningOutput.Id, "label", "20-05-05 00:00:00", "20-05-05 00:00:00");


            Event eventOutput = _eventRepository.Create(eventInput);
            Assert.True(eventOutput != null);
        }

        [Fact]
        public void Delete()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Event eventInput = new Event(0, 1, planningOutput.Id, "label", "20-05-05 00:00:00", "20-05-05 00:00:00");

            Event eventOutput = _eventRepository.Create(eventInput);
            bool isDeleted = _eventRepository.Delete(eventOutput.Id);

            Assert.True(isDeleted);
        }

        [Fact]
        public void DeleteAllFromPlanning()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Event eventInput = new Event(0, 1, planningOutput.Id, "label", "20-05-05 00:00:00", "20-05-05 00:00:00");

            Event eventOutput = _eventRepository.Create(eventInput);
            Event eventOutput2 = _eventRepository.Create(eventInput);

            bool areDeleted = _eventRepository.DeleteAllFromPlanning(_planningInput.Id);

            Assert.True(areDeleted);
        }

        [Fact]
        public void GetById()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Event eventInput = new Event(0, 1, planningOutput.Id, "label", "20-05-05 00:00:00", "20-05-05 00:00:00");

            Event eventOutput = _eventRepository.Create(eventInput);
            Event eventOutput2 = _eventRepository.Get(eventOutput.Id);

            Assert.True(eventOutput2 != null);
        }


        [Fact]
        public void Query()
        {
            IEnumerable<Event> events = _eventRepository.Query().ToList();
            Assert.True(events.Count() > 0);
        }

        [Fact]
        public void QueryFromPlanning()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Event eventInput = new Event(0, 1, planningOutput.Id, "label", "20-05-05 00:00:00", "20-05-05 00:00:00");

            _eventRepository.Create(eventInput);
            IEnumerable<Event> events = _eventRepository.QueryFromPlanning(planningOutput.Id).ToList();

            Assert.True(events.Count() > 0);
        }

        [Fact]
        public void Update()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Event eventInput = new Event(0, 1, planningOutput.Id, "label", "20-05-05 00:00:00", "20-05-05 00:00:00");

            _eventRepository.Create(eventInput);

            Event newEvent = new Event(0, 1, 0, "labelNew", "20-05-05 00:00:00", "20-05-05 00:00:00");
            bool isUpdated = _eventRepository.Update(eventInput.Id, newEvent);

            Assert.True(isUpdated);
        }
    }
}