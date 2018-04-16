namespace Bazic.Domain.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        bool SaveChanges();
    }
}
