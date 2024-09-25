using Common.Application;

namespace Blog.Application.User.Create
{
    public class CreateUserCommand : IBaseCommand
    {
        public CreateUserCommand(string name, string family, string phoneNumber, string password,string userName, string email)
        {
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Password = password;
            UserName = userName;
            Email = email;
        }
        public string Email { get; set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
