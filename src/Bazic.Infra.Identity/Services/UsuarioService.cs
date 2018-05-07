using Bazic.Infra.Identity.Interfaces;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bazic.Infra.Identity.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> AlterarEmail(Usuario usuario, string novoEmail)
        {
            return await _userManager.ChangeEmailAsync(usuario, novoEmail, null);
        }

        public async Task<IdentityResult> AlterarSenha(Usuario usuario, string novaSenha, string senhaAtual = null)
        {
            if (senhaAtual != null)
                return await _userManager.ChangePasswordAsync(usuario, senhaAtual, novaSenha);

            return await _userManager.ResetPasswordAsync(usuario,null,novaSenha);
        }

        public async Task<IdentityResult> Atualizar(Usuario usuario)
        {
            return await _userManager.UpdateAsync(usuario);
        }

        public async Task<IdentityResult> Criar(Usuario usuario, string senha)
        {
            return await _userManager.CreateAsync(usuario, senha);
        }

        public async Task<IdentityResult> Deletar(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            return await _userManager.DeleteAsync(usuario);
        }

        public async Task<Usuario> TrazerPorId(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Usuario> TrazerPorEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }

        public async Task<SignInResult> Login(string userName, string senha)
        {
            return await _signInManager.PasswordSignInAsync(userName, senha, false, false);
        }
    }
}
