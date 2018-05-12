using System;
using System.Security.Claims;

namespace Bazic.Infra.Identity.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if(principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("UserId");
            return claim?.Value;
        }
    }
}
