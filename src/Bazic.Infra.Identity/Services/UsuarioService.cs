﻿using Bazic.Infra.Identity.Interfaces;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bazic.Infra.Identity.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuarioService(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> AlterarEmail(Usuario usuario, string novoEmail)
        {
            return await _userManager.ChangeEmailAsync(usuario, novoEmail, null);
        }

        public async Task<IdentityResult> AlterarSenha(Usuario usuario, string senhaAtual, string novaSenha)
        {
            return await _userManager.ChangePasswordAsync(usuario, senhaAtual, novaSenha);
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

    }
}