using Bazic.Application.Interfaces;
using Bazic.Application.ViewModels;
using Bazic.Domain.Core.Notifications;
using Bazic.Domain.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bazic.Service.Api.Controllers
{
    [AllowAnonymous]
    [Route("/api/Contas")]
    public class ContasController : BaseController
    {
        private readonly IContaService _contaService;
        private readonly IAspNetUser _user;
        public ContasController(IContaService contaService, IDomainNotificationHandler<DomainNotification> notifications, IAspNetUser user) : base(notifications)
        {
            _contaService = contaService;
            _user = user;
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

        [HttpPost]
        [Route("/api/Contas/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return Response();
            bool resultLogin = await _contaService.Login(model);
            if (resultLogin) return Response(null,"Login Realizado com Sucesso");
            return Response();
        }

    }
}
