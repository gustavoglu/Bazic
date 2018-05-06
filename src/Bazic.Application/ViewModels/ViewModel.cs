using Flunt.Notifications;
using Flunt.Validations;

namespace Bazic.Application.ViewModels
{
    public abstract class ViewModel : Notifiable, IValidatable
    {
        public abstract void Validate();
        public bool IsValid()
        {
            Validate();
            return Valid;
        }
    }
}
