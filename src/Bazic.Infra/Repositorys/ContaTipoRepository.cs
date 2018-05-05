using Bazic.Domain.Entitys;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Infra.Data.Context;

namespace Bazic.Infra.Data.Repositorys
{
    public class ContaTipoRepository : Repository<ContaTipo>, IContaTipoRepository
    {
        public ContaTipoRepository(BazicContext context) : base(context)
        {
        }
    }
}
