using Bazic.Domain.Core.Notifications;
using Bazic.Domain.Interfaces.UoW;
using Flunt.Notifications;
using System.Linq;

namespace Bazic.Application.Services
{
    public abstract class ServiceValidation
    {
        protected readonly IDomainNotificationHandler<DomainNotification> notifications;
        private readonly IUnitOfWork _uow;

        public ServiceValidation(IDomainNotificationHandler<DomainNotification> _notifications, IUnitOfWork uow)
        {
            notifications = _notifications;
            _uow = uow;
        }

        public void AddNotification(string key, string value)
        {
            notifications.Handler(new DomainNotification(key, value));
        }

        public bool IsValid()
        {
            return notifications.HasNotification();
        }

        protected bool notifiableValidation(Notifiable notifiable)
        {
            if (notifiable.Valid) return true;
            notifiable.Notifications
                .ToList()
                .ForEach(n => AddNotification(n.Property, n.Message));
            return false;
        }

        protected bool SaveChanges()
        {
            if (_uow.SaveChanges()) return true;
            AddNotification("Commit", "Um erro ocorreu ao atualizar o banco de dados");
            return false;
        }

    }
}
