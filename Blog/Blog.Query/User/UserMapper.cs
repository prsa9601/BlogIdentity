using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.User.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.User
{
    public static class UserMapper
    {
        public static UserDto Map(this Domain.UserAgg.User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                Family = user.Family,
                PhoneNumber = user.PhoneNumber!,
                AvatarName = user.AvatarName,
                UserName = user.UserName!,
                Name = user.Name,
                Password = user.Password,
                Roles = user.Roles.Select(s => new UserRoleDto()
                {
                    RoleName = s.RoleName,
                    Name = ""
                }).ToList()
            };
        }

        public static async Task<UserDto> SetUserRoleTitles(this UserDto userDto, BlogContext context)
        {
           // var roleId = await context.UserRoles.Where(i => i.UserId == userDto.Id).Select(i => i.RoleId).ToListAsync();
            var roleNames = userDto.Roles.Select(r => r.RoleName);
            var result = await context.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();
            var roles = new List<UserRoleDto>();
            foreach (var role in result)
            {
                roles.Add(new UserRoleDto()
                {
                    RoleName = role.Name!,
                    Name = role.Name
                });
            }

            userDto.Roles = roles;
            return userDto;
        }

        public static UserFilterData MapFilterData(this Domain.UserAgg.User user)
         {
            return new UserFilterData()
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                Family = user.Family,
                PhoneNumber = user.PhoneNumber,
                AvatarName = user.AvatarName,
                UserName = user.UserName,
                Name = user.Name
            };
        }
    }
}