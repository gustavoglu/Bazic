using Bazic.Domain.Entitys;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace Bazic.Infra.Data.Repositorys
{
    public class AcessoGrupo_AcessoRepository : Repository<AcessoGrupo_Acesso>, IAcessoGrupo_AcessoRepository
    {
        public AcessoGrupo_AcessoRepository(BazicContext context) : base(context)
        {
        }

        public IEnumerable<AcessoGrupo_Acesso> TrazerPorGrupo(Guid id_grupo)
        {
            return Pesquisar(aga => aga.Id_acessoGrupo == id_grupo);
        }
    }
}
