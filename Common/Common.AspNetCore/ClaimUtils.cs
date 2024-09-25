using System.Security.Claims;

namespace Common.AspNetCore;

public static class ClaimUtils
{
    public static long GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return Convert.ToInt64(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
    public static string GetUserIdToString(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return Convert.ToString(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;
    }
    public static string GetPhoneNumber(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        var result = principal.FindFirst(ClaimTypes.MobilePhone)?.Value;
        if (result == null)
            return "";
        return result;
    }
}