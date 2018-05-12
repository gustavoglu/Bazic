using Bazic.Domain.Core.Notifications;
using Bazic.Domain.Interfaces.User;
using Bazic.Infra.Identity.Acessos;
using Bazic.Infra.Identity.Interfaces;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bazic.Infra.Identity.Services
{
    public class AcessosService : IAcessosService
    {
        private List<Acesso> Acessos { get { return AcessosList.Acessos; } }
        private readonly IAspNetUser _user;
        private readonly UserManager<Usuario> _userManager;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        public AcessosService(IAspNetUser user, UserManager<Usuario> userManager ,IDomainNotificationHandler<DomainNotification> notifications)
        {
            _user = user;
            _userManager = userManager;
            _notifications = notifications;
        }

        private void NotificaErrosUserManager(IdentityResult result)
        {
               result.Errors.ToList().ForEach( erro => _notifications.Handler(new DomainNotification("Usuario", erro.Description)));            
        }

        public async Task<bool> AdicionarContaAdmin(Guid id_conta)
        {
            var usuario = await _userManager.FindByIdAsync(id_conta.ToString());
            if(usuario == null)
            {
                _notifications.Handler(new DomainNotification("Usuario", "Usuario não encontrado"));
                return false;
            }

            var claims = await _userManager.GetClaimsAsync(usuario);

            if (claims.ToList().Exists(c => c.Type == "Admin" && c.Value == "Admin")) return true;

            var result = await _userManager.AddClaimAsync(usuario, new Claim("Admin", "Admin"));
            if (result.Succeeded) return true;
            NotificaErrosUserManager(result);
            return false;
        }

        public async Task<bool> AtualizarAcessosConta(Guid id_conta, List<Acesso> acessos)
        {
            var usuario = await _userManager.FindByIdAsync(id_conta.ToString());
            if (usuario == null)
            {
                _notifications.Handler(new DomainNotification("Usuario", "Usuario não encontrado"));
                return false;
            }

            var claims = await _userManager.GetClaimsAsync(usuario);
            var acessosConta = await TrazerAcessos(id_conta);

            var acessosNaoConcedidos = from acesso in acessos
                                       select new Acesso(acesso.Descricao, acesso.Opcoes.Where(o => !o.Concedido));

            var claimsParaSeremRemovidas = (from claim in claims
                                            from acessoNovo in acessosNaoConcedidos
                                            from opcao in acessoNovo.Opcoes
                                            where claim.Value == opcao.Descricao && claim.Type == acessoNovo.Descricao
                                            select claim
                                           ).Distinct();

            var claimsParaSeremInseridas = from acessoNovo in acessos
                                           from opcao in acessoNovo.Opcoes
                                           where !claims.ToList().Exists(c => c.Type == acessoNovo.Descricao && c.Value == opcao.Descricao) &&
                                                 opcao.Concedido
                                           select new Claim(acessoNovo.Descricao,opcao.Descricao,opcao.Descricao);

            if (!claimsParaSeremInseridas.Any() && !claimsParaSeremRemovidas.Any())
                return true;

            if (claimsParaSeremRemovidas.Any())
            {
                var resultRemocao = await _userManager.RemoveClaimsAsync(usuario, claimsParaSeremRemovidas);

                if (!resultRemocao.Succeeded)
                {
                    NotificaErrosUserManager(resultRemocao);
                    return false;
                }
            }

            if (claimsParaSeremInseridas.Any())
            {
                var resultInsercao = await _userManager.AddClaimsAsync(usuario, claimsParaSeremInseridas);
                if (!resultInsercao.Succeeded)
                {
                    NotificaErrosUserManager(resultInsercao);
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> RemoverContaAdmin(Guid id_conta)
        {
            var usuario = await _userManager.FindByIdAsync(id_conta.ToString());
            if (usuario == null)
            {
                _notifications.Handler(new DomainNotification("Usuario", "Usuario não encontrado"));
                return false;
            }

            var claims = await _userManager.GetClaimsAsync(usuario);

            var claimAdmin = claims.ToList().FirstOrDefault(c => c.Type == "Admin" && c.Value == "Admin");

            if (claimAdmin == null) return true;

            var result = await _userManager.RemoveClaimAsync(usuario, claimAdmin);
            if (result.Succeeded) return true;
            NotificaErrosUserManager(result);
            return false;
        }

        public async Task<IEnumerable<Acesso>> TrazerAcessos(Guid id_conta)
        {
            var claims = await TrazerClaimsPorConta(id_conta);
            var acessos = Acessos;
            acessos.ForEach(a => AtualizaEstruturaAcesso(a, claims.ToList()));
            return acessos;
        }

        private async Task<List<Claim>> TrazerClaimsPorConta(Guid id_conta)
        {
            var usuario = await _userManager.FindByIdAsync(id_conta.ToString());
            if (usuario == null)
            {
                _notifications.Handler(new DomainNotification("Usuario", "Usuario não encontrado"));
                return null;
            }

            var claims = await _userManager.GetClaimsAsync(usuario);
            return claims.ToList();
        }

        private void AtualizaEstruturaAcesso(Acesso acesso, List<Claim> claimsConta)
        {
            foreach (var opcao in acesso.Opcoes)
            {
                if (claimsConta.Exists(c => c.Type == acesso.Descricao && c.Value == opcao.Descricao))
                    opcao.ConcederAcesso();
            }
        }

    }
}
