using Blog.Domain.RoleAgg.Repository;
using Blog.Domain.UserAgg.Repository;
using Common.Application;
using Microsoft.AspNetCore.Identity;

namespace Blog.Application.User.SetRole
{
    public record class SetRoleCommand(string UserName, List<string> RolesName) : IBaseCommand;
   
    public class SetRoleCommandHandler : IBaseCommandHandler<SetRoleCommand>
    {
        private readonly IRoleRepository<Domain.RoleAgg.Role> _repository;
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;
        private readonly UserManager<Domain.UserAgg.User> _userManager;

        public SetRoleCommandHandler(IRoleRepository<Domain.RoleAgg.Role> repository, IUserRepository<Domain.UserAgg.User> userRepository, UserManager<Domain.UserAgg.User> userManager)
        {
            _repository = repository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<OperationResult> Handle(SetRoleCommand request, CancellationToken cancellationToken)
        {
            //var role = await _repository.GetTracking(request.roleId);
            //if (role == null) 
            //    return OperationResult.NotFound();
            
            var user = await _userRepository.GetTrackingByUserName(request.UserName);
          
            if(user == null)
                return OperationResult.NotFound();
            user.SetUserRoles(request.RolesName);
            
            var result = await _userManager.AddToRolesAsync(user, request.RolesName);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
