using System.Linq;
using System.Collections.Generic;
using TI_BackEnd.Domain.User;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;
using Xunit;

namespace unit_tests
{
    public class UserTests
    {
        private UserRepository _userRepository = new UserRepository();
        private User _userInput = new User(0, "email", "lastname", "firstname", "username", "password");

        [Fact]
        public void Create()
        {
            User userOutPut = _userRepository.Create(_userInput);

            Assert.True(userOutPut != null);
        }

        [Fact]
        public void GetAuthentication()
        {
            User userOutPut = _userRepository.Create(_userInput);
            User userOutPut2 = _userRepository.GetAuthentication("email", "password");

            Assert.True(userOutPut2 != null);
        }

        [Fact]
        public void GetByEmail()
        {
            User userOutPut = _userRepository.Create(_userInput);
            User userOutPut2 = _userRepository.Get(userOutPut.Email);

            Assert.True(userOutPut2 != null);
        }

        [Fact]
        public void Query()
        {
            User userOutPut = _userRepository.Create(_userInput);
            IEnumerable<User> users = _userRepository.Query().ToList();

            Assert.True(users.Count() > 0);
        }
    }
}