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
using Microsoft.AspNetCore.Http;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

// This is a Token Example controller to generate the token to your API
// To access use for ex Postman and call: http://localhost:{port}/api/token

namespace Bazic.Service.Api.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : BaseController
    {
        private readonly IContaService _contaService;
        private readonly IUsuarioService _usuarioService;
        private readonly SignInManager<Usuario> _sign;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor _accessor;
        public TokenController(IDomainNotificationHandler<DomainNotification> notifications, IContaService contaService, IUsuarioService usuarioService, IHttpContextAccessor accessor, SignInManager<Usuario> sign, UserManager<Usuario> userManager) : base(notifications)
        {
            _contaService = contaService;
            _usuarioService = usuarioService;
            _accessor = accessor;
            _sign = sign;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations, [FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid) return Response();
            bool resultLogin = await _contaService.Login(model);
            if (!resultLogin) return Response();
            var usuario = await _usuarioService.TrazerPorEmail(model.UserName);
            string userId = usuario.Id;
            var claims = await _userManager.GetClaimsAsync(usuario);
            claims.Add(new Claim("id_usuario", userId));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, userId));
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(userId, "UserId"),claims);

            DateTime dtCreation = DateTime.Now;
            DateTime dtExpiration = dtCreation + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken t = new JwtSecurityToken(
                issuer: tokenConfigurations.Issuer,
                audience: tokenConfigurations.Audience,
                signingCredentials: signingConfigurations.SigningCredentials,
                claims: identity.Claims,
                notBefore: dtCreation,
                expires: dtExpiration);


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


            return Response(new
            {
                authenticated = true,
                created = dtCreation.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dtExpiration.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                id_usuario = usuario.Id,
                userName = usuario.UserName,
                message = "OK"
            }, "Login realizado com sucesso");

            // case of fail:
            //return new
            //{
            //    authenticated = false,
            //    message = "Fail to authenticate"
            //};
        }
    }
}
