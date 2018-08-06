using Bazic.Domain.Entitys;
using System;
using System.Collections.Generic;

namespace Bazic.Domain.Interfaces.Repositorys
{
    public interface IAcessoGrupo_AcessoRepository : IRepository<AcessoGrupo_Acesso>
    {
        IEnumerable<AcessoGrupo_Acesso> TrazerPorGrupo(Guid id_grupo);
    }
}
