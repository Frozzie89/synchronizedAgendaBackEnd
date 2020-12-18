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
        private User userInput = new User(0, "email", "lastname", "firstname", "username", "password");

        [Fact]
        public void Create()
        {
            User userOutPut = _userRepository.Create(userInput);

            Assert.True(userOutPut != null);
        }

        [Fact]
        public void GetAuthentication()
        {
            User userInDb = _userRepository.Create(userInput);
            User userOutPut = _userRepository.GetAuthentication("email", "password");

            Assert.True(userOutPut != null);
        }

        [Fact]
        public void GetByEmail()
        {
            User userInDb = _userRepository.Create(userInput);
            User userOutPut = _userRepository.Get("email");

            Assert.True(userOutPut != null);
        }

        [Fact]
        public void Query()
        {
            User userInDb = _userRepository.Create(userInput);
            IEnumerable<User> users = _userRepository.Query().ToList();

            Assert.True(users.Count() > 0);
        }
    }
}