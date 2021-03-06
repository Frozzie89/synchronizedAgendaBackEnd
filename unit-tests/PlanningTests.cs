using System.Collections.Generic;
using System.Linq;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using Xunit;

namespace unit_tests
{
    public class PlanningTests
    {
        private PlanningRepository _planningRepository = new PlanningRepository();
        private Planning _planningInput = new Planning(0, "labelPlanning", 0);

        [Fact]
        public void Create()
        {
            Planning planningOutPut = _planningRepository.Create(_planningInput);
            Assert.True(planningOutPut != null);
        }

        [Fact]
        public void GetByLabel()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Planning planningOutput2 = _planningRepository.GetByLabel(planningOutput.LabelPlanning);
            Assert.True(planningOutput2 != null);
        }

        [Fact]
        public void GetBySuperUser()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Planning planningOutput2 = _planningRepository.GetBySuperUser(planningOutput.IdSuperUser);
            Assert.True(planningOutput2 != null);
        }

        [Fact]
        public void Query()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            IEnumerable<Planning> planningsOutput = _planningRepository.Query().ToList();
            Assert.True(planningsOutput.Count() > 0);
        }
    }
}