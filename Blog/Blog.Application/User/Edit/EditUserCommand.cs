using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.User.Edit
{
    public class EditUserCommand : IBaseCommand
    {
        public EditUserCommand(long userId, IFormFile? avatar, string name, string family, string phoneNumber, string userName)
        {
            UserId = userId;
            Avatar = avatar;
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            UserName = userName;
        }
        public long UserId { get; set; }
        public IFormFile? Avatar { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string UserName { get; private set; }
    }
}
