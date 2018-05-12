using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Bazic.Domain.Interfaces.User
{
    public interface IAspNetUser
    {
        string GetUserAuthenticateName();
        Guid? GetUserAuthenticateId();
        List<Claim> GetUserAuthenticateClaims();
        bool IsAuthenticate();
    }
}
