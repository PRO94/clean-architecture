using System.Security.Claims;

namespace Bookify.Infrastructure.Authorization;

internal static class ClaimsPrincipalExtensions
{
    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
            throw new ApplicationException("User identity is unavailable");
    }
}