using Bazic.Domain.Core.Notifications;
using Bazic.Domain.Interfaces.User;
using Bazic.Infra.Identity.Interfaces;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bazic.Service.Api.Controllers
{

    [Route("api/Contas/[controller]")]
    public class AcessosController : BaseController
    {
        private readonly IAcessosService _acessosService;
        private readonly IAspNetUser _user;
        public AcessosController(IDomainNotificationHandler<DomainNotification> notifications, IAcessosService acessosService, IAspNetUser user) : base(notifications)
        {
            _acessosService = acessosService;
            _user = user;
        }

        [HttpGet]
        //[AllowAnonymous]
        [Authorize(Policy = "VisualizarAcessos")]
        [Route("/api/Contas/[controller]/{id_conta:Guid}")]
        public async Task<IActionResult> Get(Guid id_conta)
        {
            var usuario = HttpContext;
            return Response( await _acessosService.TrazerAcessos(id_conta));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/Contas/[controller]/{id_conta:Guid}")]
        public async Task<IActionResult> Post(Guid id_conta, [FromBody] List<Acesso> acessos)
        {
            var claims = _user.GetUserAuthenticateId();
           
            return Response(await _acessosService.AtualizarAcessosConta(id_conta,acessos));
        }
    }
}
