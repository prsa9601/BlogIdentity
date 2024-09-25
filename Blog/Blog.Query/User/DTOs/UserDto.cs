using Common.Query;
using Common.Query.Filter;
using Microsoft.AspNetCore.Identity;

namespace Blog.Query.User.DTOs;

public class UserDto 
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string AvatarName { get; set; } = "Avatar.png";
    public List<UserRoleDto> Roles { get; set; } = new List<UserRoleDto>();
}
public class UserProfileDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AvatarName { get; set; } = string.Empty;
}
public class UserRoleDto : IdentityRole
{
    public string RoleName { get; set; } = string.Empty;
}


public class UserFilterData : IdentityUser
{
    public DateTime CreationDate { get; set; }
   // public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    //public string? PhoneNumber { get; set; }
    // public string? UserName { get; set; } 
    public string AvatarName { get; set; } = string.Empty;
}

public class UserFilterParams:BaseFilterParam
{
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
}
public class UserFilterResult : BaseRoleFilter<UserFilterData,UserFilterParams>
{

}
public class BaseRoleFilter<TData, TParam> : BaseFilter
    where TParam : BaseFilterParam
    where TData : IdentityUser
{
    public List<TData> Data { get; set; } = new List<TData>();

    public TParam? FilterParams { get; set; } 
}