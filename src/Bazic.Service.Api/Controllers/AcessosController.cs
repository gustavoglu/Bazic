using Bazic.Domain.Core.Notifications;
using Bazic.Infra.Identity.Interfaces;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bazic.Service.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/Contas/[controller]")]
    public class AcessosController : BaseController
    {
        private readonly IAcessosService _acessosService;
        public AcessosController(IDomainNotificationHandler<DomainNotification> notifications, IAcessosService acessosService) : base(notifications)
        {
            _acessosService = acessosService;
        }

        [HttpGet]
        //[Authorize(Policy = "VisualizarAcessos")]
        [Route("/api/Contas/[controller]/{id_conta:Guid}")]
        public async Task<IActionResult> Get(Guid id_conta)
        {
            return Response( await _acessosService.TrazerAcessos(id_conta));
        }

        [HttpPost]
        [Route("/api/Contas/[controller]/{id_conta:Guid}")]
        public async Task<IActionResult> Post(Guid id_conta, [FromBody] List<Acesso> acessos)
        {
            return Response(await _acessosService.AtualizarAcessosConta(id_conta,acessos));
        }
    }
}
