namespace TI_BackEnd.Domain.User
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(int id, string email, string lastName, string firstName, string userName, string password)
        {
            Id = id;
            Email = email;
            LastName = lastName;
            FirstName = firstName;
            UserName = userName;
            Password = password;
        }
    }
}