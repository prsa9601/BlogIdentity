

namespace Blog.Domain.UserAgg.Services
{
    public interface IUserDomainService
    {
        bool PhoneNumberIsExist(string phoneNumber);
        bool UserNameIsExist(string userName);

    }
}
