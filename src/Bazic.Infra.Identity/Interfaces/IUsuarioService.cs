using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Bazic.Infra.Identity.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task<IdentityResult> Criar(Usuario usuario, string senha);
        Task<IdentityResult> AlterarSenha(Usuario usuario, string senhaAtual ,string novaSenha);
        Task<IdentityResult> AlterarEmail(Usuario usuario, string novoEmail);
        Task<IdentityResult> Deletar(string id);
        Task<IdentityResult> Atualizar(Usuario usuario);
        Task<Usuario> TrazerPorId(string id);
        Task<Usuario> TrazerPorEmail(string email);
    }
}
