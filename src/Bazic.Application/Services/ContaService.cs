using Bazic.Application.Interfaces;
using Bazic.Application.ViewModels;
using Bazic.Domain.Entitys;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Infra.Identity.Interfaces;
using Bazic.Infra.Identity.Models;
using Flunt.Notifications;
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
        public ContaService(IContaRepository contaRepository, IUsuarioService usuarioService)
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
            var result = await _usuarioService.AlterarSenha(usuario,novaSenha,senhaAtual);
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
            if (!result.Succeeded) { AdicionaErrosIdentityResylt(result); return null; };
            var contaCriada = await _contaRepository.Criar(conta);
            return contaCriada;
        }

        public bool Deletar(Guid id_conta)
        {
            throw new NotImplementedException();
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }

        private void AdicionaErrosIdentityResylt(IdentityResult identityResult)
        {
            if (identityResult.Succeeded) return;
            identityResult.Errors.ToList().ForEach(e => AddNotification(new Notification("Identity", e.Description)));
        }
    }
}
