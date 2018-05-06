using Bazic.Domain.Interfaces.UoW;
using Bazic.Infra.Data.Context;

namespace Bazic.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BazicContext _context;
        public UnitOfWork(BazicContext context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
