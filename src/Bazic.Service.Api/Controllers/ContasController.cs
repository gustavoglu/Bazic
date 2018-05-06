using Bazic.Application.Interfaces;
using Bazic.Application.ViewModels;
using Bazic.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bazic.Service.Api.Controllers
{
    [Route("/api/Contas")]
    public class ContasController : BaseController
    {
        private readonly IContaService _contaService;
        public ContasController(IContaService contaService, IDomainNotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _contaService = contaService;
        }

        [HttpPost]
        [Route("/api/Contas/Nova")]
        public async Task<IActionResult> NovaConta([FromBody]NovaContaViewModel model)
        {
            if (!model.IsValid())
            {
                AddErrosNotifiableInNotifications(model);
                return Response();
            }

            var contaCriada = await _contaService.Criar(model);
            if (contaCriada == null) return Response();
            return Response(contaCriada, "Conta Criada com Sucesso");
        }
    }
}
