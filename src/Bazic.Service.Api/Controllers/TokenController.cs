using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Bazic.Service.Api.JWTConfig;
using Bazic.Application.ViewModels;
using Bazic.Domain.Core.Notifications;
using Bazic.Application.Interfaces;
using System.Threading.Tasks;
using Bazic.Infra.Identity.Interfaces;

// This is a Token Example controller to generate the token to your API
// To access use for ex Postman and call: http://localhost:{port}/api/token/auth

namespace Bazic.Service.Api.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : BaseController
    {
        private readonly IContaService _contaService;
        private readonly IUsuarioService _usuarioService;
        public TokenController(IDomainNotificationHandler<DomainNotification> notifications, IContaService contaService, IUsuarioService usuarioService) : base(notifications)
        {
            _contaService = contaService;
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post( [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations, [FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid) return Response();
            bool resultLogin = await _contaService.Login(model);
            if (!resultLogin) return Response();
            var usuario = await _usuarioService.TrazerPorEmail(model.UserName);
            string userId = usuario.Id;

            ClaimsIdentity identity = new ClaimsIdentity(
                       new GenericIdentity(userId, "Login"),
                       new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userId)
                       }
                   );

            DateTime dtCreation = DateTime.Now;
            DateTime dtExpiration = dtCreation + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dtCreation,
                Expires = dtExpiration
            });
            var token = handler.WriteToken(securityToken);

            return Response( new
            {
                authenticated = true,
                created = dtCreation.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dtExpiration.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            },"Login realizado com sucesso");

            // case of fail:
            //return new
            //{
            //    authenticated = false,
            //    message = "Fail to authenticate"
            //};
        }
    }
}
