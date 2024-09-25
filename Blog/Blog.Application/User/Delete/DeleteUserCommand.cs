using Blog.Domain.UserAgg.Repository;
using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.User.Delete
{
    public record class DeleteUserCommand(long id) : IBaseCommand
    {
    }
    public class DeleteUserCommandHandler : IBaseCommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;

        public DeleteUserCommandHandler(IUserRepository<Domain.UserAgg.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.Delete(request.id);
            if (!result)
                return OperationResult.Error();
            return OperationResult.Success();
        }
    }

}
