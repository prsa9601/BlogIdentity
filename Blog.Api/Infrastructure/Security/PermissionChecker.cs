using System.Resources;
using Blog.Domain.RoleAgg.Enums;
using Blog.Presentation.Facade.Role;
using Blog.Presentation.Facade.User;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Api.Infrastructure.Security;

public class PermissionChecker(Permission permission) : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private IUserFacade _userFacade = null!;
    private IRoleFacade _roleFacade = null!;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();
        _roleFacade = context.HttpContext.RequestServices.GetRequiredService<IRoleFacade>();
        if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (await UserHasPermission(context) == false)
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult("Unauthorize");
        }
    }

    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        //comment
        if (_roleFacade == null && _userFacade == null)
        {
            return false;
            throw new Exception("مشکل سمت سرور به وجود آمده");
        }
        var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
        bool hasAllowAnonymous = false;
        foreach (var f in metaData)
        {
            try
            {
                hasAllowAnonymous = f.TypeId.Name == "AllowAnonymousAttribute";
                if (hasAllowAnonymous)
                    break;
            }
            catch
            {
                // ignored
            }
        }

        return hasAllowAnonymous;
    }
    private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
    {
        var user = await _userFacade.GetUserById(context.HttpContext.User.GetUserIdToString());
        if (user == null)
            return false;

        var roleNames = user.Roles.Select(s => s.RoleName).ToList();
        var roles = await _roleFacade.GetRoles();
        if (roles == null)
        {
            return false;
        }
        var userRoles = roles.Where(i => roleNames.Contains(i.Name)).ToList();

        return userRoles.Any(i => i.Permissions.Contains(permission));
    }
}