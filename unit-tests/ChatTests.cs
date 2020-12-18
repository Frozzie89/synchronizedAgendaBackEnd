using System.Collections.Generic;
using TI_BackEnd.Domain.Chat;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Infrastructure.SqlServer.ChatDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using Xunit;

namespace unit_tests
{
    public class ChatTests
    {
        ChatRepository _chatRepository = new ChatRepository();
        PlanningRepository _planningRepository = new PlanningRepository();
        Planning _planningInput = new Planning(0, "label", 0);

        [Fact]
        public void Create()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Chat chatInput = new Chat(0, planningOutput.Id);
            Chat chatOutput = _chatRepository.Create(chatInput);

            Assert.True(chatOutput != null);
        }

        [Fact]
        public void Delete()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Chat chatInput = new Chat(0, planningOutput.Id);
            Chat chatOutput = _chatRepository.Create(chatInput);

            bool isDeleted = _chatRepository.Delete(chatOutput.Id);

            Assert.True(isDeleted);
        }

        [Fact]
        public void DeleteByPlanningId()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Chat chatInput = new Chat(0, planningOutput.Id);
            Chat chatOutput = _chatRepository.Create(chatInput);

            bool areDeleted = _chatRepository.DeleteByPlanningId(planningOutput.Id);

            Assert.True(areDeleted);
        }

        [Fact]
        public void GetById()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Chat chatInput = new Chat(0, planningOutput.Id);
            Chat chatOutput = _chatRepository.Create(chatInput);

            Chat chatOutput2 = _chatRepository.Get(chatOutput.Id);

            Assert.True(chatOutput2 != null);
        }

        [Fact]
        public void GetByPlanningId()
        {
            Planning planningOutput = _planningRepository.Create(_planningInput);
            Chat chatInput = new Chat(0, planningOutput.Id);
            Chat chatOutput = _chatRepository.Create(chatInput);

            Chat chatOutput2 = _chatRepository.GetByPlanningId(planningOutput.Id);

            Assert.True(chatOutput2 != null);
        }
    }
}