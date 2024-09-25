using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.UserAgg.Repository;
using Blog.Domain.UserAgg.Services;

namespace Blog.Application.User
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        public UserDomainService(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }
        public bool PhoneNumberIsExist(string phoneNumber)
        {
            return _repository.Exists(u => u.PhoneNumber == phoneNumber);
        }

        public bool UserNameIsExist(string userName)
        {
            return _repository.Exists(u => u.UserName == userName);
        }
    }
}
