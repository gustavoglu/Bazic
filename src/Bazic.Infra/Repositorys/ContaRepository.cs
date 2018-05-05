using Bazic.Domain.Entitys;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Infra.Data.Context;

namespace Bazic.Infra.Data.Repositorys
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(BazicContext context) : base(context)
        {
        }
    }
}
