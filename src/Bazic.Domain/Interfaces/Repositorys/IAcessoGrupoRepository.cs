using Bazic.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bazic.Domain.Interfaces.Repositorys
{
    public interface IAcessoGrupoRepository : IRepository<AcessoGrupo>
    {
        IEnumerable<AcessoGrupo> TrazerPorConta(Guid id_conta);
    }
}
