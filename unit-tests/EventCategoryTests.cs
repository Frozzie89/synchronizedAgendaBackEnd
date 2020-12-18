using System.Collections.Generic;
using System.Linq;
using TI_BackEnd.Domain.EventCategory;
using TI_BackEnd.Infrastructure.SqlServer.EventCategoryDAO;
using Xunit;

namespace unit_tests
{
    public class EventCategoryTests
    {
        private EventCategoryRepository _eventCategoryRepository = new EventCategoryRepository();

        [Fact]
        public void Query()
        {
            IEnumerable<EventCategory> eventCategoriesOutput = _eventCategoryRepository.Query().ToList();
            Assert.True(eventCategoriesOutput.Count() > 0);
        }

        [Fact]
        public void TestName()
        {
            EventCategory eventCategoryOutput = _eventCategoryRepository.Get(1);
            Assert.True(eventCategoryOutput != null);
        }
    }
}