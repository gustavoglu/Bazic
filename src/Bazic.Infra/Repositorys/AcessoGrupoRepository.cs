using System;
using System.Collections.Generic;
using Bazic.Domain.Entitys;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Infra.Data.Context;

namespace Bazic.Infra.Data.Repositorys
{
    public class AcessoGrupoRepository : Repository<AcessoGrupo>, IAcessoGrupoRepository
    {
        public AcessoGrupoRepository(BazicContext context) : base(context)
        {
        }

        public IEnumerable<AcessoGrupo> TrazerPorConta(Guid id_conta)
        {
            return null;
        }
    }
}
