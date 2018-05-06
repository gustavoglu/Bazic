using Bazic.Application.Interfaces;
using Bazic.Application.ViewModels;
using Bazic.Domain.Core.Notifications;
using Bazic.Domain.Entitys;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Domain.Interfaces.UoW;
using Bazic.Infra.Identity.Interfaces;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bazic.Application.Services
{
    public class ContaService : ServiceValidation, IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IUsuarioService _usuarioService;
        public ContaService(IContaRepository contaRepository, IUsuarioService usuarioService, IUnitOfWork uow, IDomainNotificationHandler<DomainNotification> notifications) : base(notifications,uow)
        {
            _contaRepository = contaRepository;
            _usuarioService = usuarioService;
        }
        public Task<bool> AlterarEmail(Conta conta, string novoEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AlterarSenha(Conta conta, string novaSenha, string senhaAtual = null)
        {
            var usuario = await _usuarioService.TrazerPorId(conta.Id.ToString());
            if (usuario == null) return false;
            var result = await _usuarioService.AlterarSenha(usuario, novaSenha, senhaAtual);
            if (!result.Succeeded) return false;
            return true;
        }

        public Task<Conta> Atualizar(Conta conta)
        {
            throw new NotImplementedException();
        }

        public async Task<Conta> Criar(NovaContaViewModel model)
        {
            Conta conta = new Conta { NomeCompleto = model.NomeCompleto, Id_contaTipo = Guid.Parse("D43849B6-3A8E-42BF-84A2-0A16E70D6D8D") };
            string id_usuario = conta.Id.ToString();
            Usuario usuario = new Usuario { Id = id_usuario, Email = model.Email, UserName = model.Email };
            var result = await _usuarioService.Criar(usuario, model.Senha);
            if (!result.Succeeded) { AdicionaErrosIdentityResult(result); return null; };
            _contaRepository.Criar(conta);
            bool resultSave = SaveChanges();
            if (!resultSave) return null;           
            return await _contaRepository.TrazerPorId(conta.Id);
        }

        public bool Deletar(Guid id_conta)
        {
            throw new NotImplementedException();
        }

        private void AdicionaErrosIdentityResult(IdentityResult identityResult)
        {
            if (identityResult.Succeeded) return;
            identityResult.Errors.ToList().ForEach(e => AddNotification("Identity", e.Description));
        }

        

    }
}
