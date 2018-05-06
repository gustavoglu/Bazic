using Bazic.Application.Interfaces;
using Bazic.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bazic.Service.Api.Controllers
{
    [Route("/api/Contas")]
    public class ContasController : BaseController
    {
        private readonly IContaService _contaService;
        public ContasController(IContaService contaService)
        {
            _contaService = contaService;
        }
        [HttpPost]
        [Route("/api/Contas/Nova")]
        public async Task<IActionResult> NovaConta([FromBody]NovaContaViewModel model)
        {
            model.Validate();
            if (!model.Valid)
            {
                ErrosModelStateNotification(model);
                return Response();
            }

            var contaCriada = await _contaService.Criar(model);
            return Response(contaCriada, "Conta Criada com Sucesso");
        }
    }
}
