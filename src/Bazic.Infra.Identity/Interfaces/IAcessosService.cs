using Bazic.Infra.Identity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bazic.Infra.Identity.Interfaces
{
    public interface IAcessosService
    {
        Task<IEnumerable<Acesso>> TrazerAcessos(Guid id_conta);
        Task<bool> AdicionarContaAdmin(Guid id_conta);
        Task<bool> RemoverContaAdmin(Guid id_conta);
        Task<bool> AtualizarAcessosConta(Guid id_conta, List<Acesso> acessos);
    }
}
