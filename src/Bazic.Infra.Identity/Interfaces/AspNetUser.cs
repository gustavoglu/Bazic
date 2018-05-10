using Bazic.Domain.Interfaces.User;
using Bazic.Infra.Identity.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Bazic.Infra.Identity.Interfaces
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public List<Claim> GetUserAuthenticateClaims()
        {
            return _accessor.HttpContext.User.Claims.ToList();
        }

        public Guid GetUserAuthenticateId()
        {
            return Guid.Parse(_accessor.HttpContext.User.GetUserId());
        }

        public string GetUserAuthenticateName()
        {
            return _accessor.HttpContext.User.Identity.Name;
        }

        public bool IsAuthenticate()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
