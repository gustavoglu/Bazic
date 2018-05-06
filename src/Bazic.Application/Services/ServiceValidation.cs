using Flunt.Notifications;
using Flunt.Validations;

namespace Bazic.Application.Services
{
    public abstract class ServiceValidation : Notifiable, IValidatable
    {
        public abstract void Validate();
    }
}
