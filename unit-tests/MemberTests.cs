using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using TI_BackEnd.Domain.Member;
using TI_BackEnd.Domain.Planning;
using TI_BackEnd.Domain.User;
using TI_BackEnd.Infrastructure.SqlServer.MemberDAO;
using TI_BackEnd.Infrastructure.SqlServer.PlanningDAO;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;
using Xunit;

namespace unit_tests
{
    public class MemberTests
    {
        private MemberRepository _memberRepository = new MemberRepository();
        private UserRepository _userRepository = new UserRepository();
        private PlanningRepository _planningRepository = new PlanningRepository();

        private User _userInput = new User(0, "email", "lastName", "firstName", "userName", "password");
        private User _userInput2 = new User(0, "email2", "lastName2", "firstName2", "userName2", "password2");
        private Planning _planningInput = new Planning(0, "label", 0);
        private Planning _planningInput2 = new Planning(0, "label2", 0);

        [Fact]
        public void Create()
        {
            User userOutput = _userRepository.Create(_userInput);
            Planning planningOutput = _planningRepository.Create(_planningInput);

            Member memberInput = new Member(userOutput.Id, planningOutput.Id, false);
            Member memberOutput = _memberRepository.Create(memberInput);

            Assert.True(memberOutput != null);
        }

        [Fact]
        public void Delete()
        {
            User userOutput = _userRepository.Create(_userInput);
            Planning planningOutput = _planningRepository.Create(_planningInput);

            Member memberInput = new Member(userOutput.Id, planningOutput.Id, false);
            Member memberOutput = _memberRepository.Create(memberInput);

            bool isDeleted = _memberRepository.Delete(memberOutput.IdUser, memberOutput.IdPlanning);

            Assert.True(isDeleted);
        }

        [Fact]
        public void Get()
        {
            User userOutput = _userRepository.Create(_userInput);
            Planning planningOutput = _planningRepository.Create(_planningInput);

            Member memberInput = new Member(userOutput.Id, planningOutput.Id, false);
            Member memberOutput = _memberRepository.Create(memberInput);

            Member memberOutput2 = _memberRepository.Get(memberOutput.IdUser, memberOutput.IdPlanning);

            Assert.True(memberOutput2 != null);
        }

        [Fact]
        public void Query()
        {
            User userOutput = _userRepository.Create(_userInput);
            Planning planningOutput = _planningRepository.Create(_planningInput);

            Member memberInput = new Member(userOutput.Id, planningOutput.Id, false);
            Member memberOutput = _memberRepository.Create(memberInput);

            IEnumerable<Member> members = _memberRepository.Query().ToList();

            Assert.True(members.Count() > 0);
        }

        [Fact]
        public void QueryFromPlanning()
        {
            User userOutput1 = _userRepository.Create(_userInput);
            User userOutput2 = _userRepository.Create(_userInput2);
            Planning planningOutput = _planningRepository.Create(_planningInput);

            Member memberInput1 = new Member(userOutput1.Id, planningOutput.Id, false);
            Member memberInput2 = new Member(userOutput2.Id, planningOutput.Id, true);

            Member memberOutput1 = _memberRepository.Create(memberInput1);
            Member memberOutput2 = _memberRepository.Create(memberInput2);

            IEnumerable<Member> members = _memberRepository.QueryFromPlanning(planningOutput.Id).ToList();

            Assert.True(members.Count() == 2);
        }

        [Fact]
        public void QueryFromGrantedPlanning()
        {
            User userOutput1 = _userRepository.Create(_userInput);
            User userOutput2 = _userRepository.Create(_userInput2);
            Planning planningOutput = _planningRepository.Create(_planningInput);

            Member memberInput1 = new Member(userOutput1.Id, planningOutput.Id, false);
            Member memberInput2 = new Member(userOutput2.Id, planningOutput.Id, true);

            Member memberOutput1 = _memberRepository.Create(memberInput1);
            Member memberOutput2 = _memberRepository.Create(memberInput2);

            IEnumerable<Member> members = _memberRepository.QueryFromGrantedUser(planningOutput.Id).ToList();

            Assert.True(members.Count() == 1);
        }

        [Fact]
        public void QueryFromUser()
        {
            User userOutput = _userRepository.Create(_userInput);
            Planning planningOutput = _planningRepository.Create(_planningInput);

            Member memberInput = new Member(userOutput.Id, planningOutput.Id, false);
            Member memberOutput = _memberRepository.Create(memberInput);

            IEnumerable<Member> members = _memberRepository.QueryFromUser(userOutput.Id).ToList();

            Assert.True(members.Count() > 0);
        }

        [Fact]
        public void Update()
        {
            User userOutput1 = _userRepository.Create(_userInput);
            User userOutput2 = _userRepository.Create(_userInput2);
            Planning planningOutput = _planningRepository.Create(_planningInput);

            Member memberInput = new Member(userOutput1.Id, planningOutput.Id, false);
            Member memberOutput = _memberRepository.Create(memberInput);

            Member newMember = new Member(userOutput2.Id, planningOutput.Id, false);

            bool isUpdated = _memberRepository.Update(userOutput1.Id, planningOutput.Id, newMember);

            Assert.True(isUpdated);
        }
    }
}