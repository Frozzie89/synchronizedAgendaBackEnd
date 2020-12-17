using System.Security.Cryptography;
using System.Text;
using TI_BackEnd.Infrastructure.SqlServer.UserDAO;

namespace TI_BackEnd.Services
{
    public class UserService
    {
        public UserService() { }

        public string encryptSha256(string value)
        {
            UserRepository _userRepository = new UserRepository();
            var passwordBytes = Encoding.UTF8.GetBytes(value);

            var sha = new SHA256Managed();
            var hash = sha.ComputeHash(passwordBytes);

            string hashString = "";

            foreach (var b in hash)
            {
                hashString += b.ToString("x2");
            }

            hashString = hashString.Substring(0, 50);

            return hashString;
        }
    }
}