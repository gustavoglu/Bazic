using Bazic.Application.Services;
using Bazic.Application.ViewModels;
using Bazic.Domain.Entitys;
using System;
using System.Threading.Tasks;

namespace Bazic.Application.Interfaces
{
    public interface IContaService 
    {
        Task<Conta> Criar(NovaContaViewModel conta);
        Task<Conta> Atualizar(Conta conta);
        bool Deletar(Guid id_conta); 
        Task<bool> AlterarSenha(Conta conta,string novaSenha, string senhaAtual = null);
        Task<bool> AlterarEmail(Conta conta, string novoEmail);

    }
}
