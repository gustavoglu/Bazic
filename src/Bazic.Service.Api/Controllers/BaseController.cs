using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bazic.Service.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Notifications = new List<Notification>();
        }

        protected List<Notification> Notifications { get; set; }

        protected bool HasNotification { get { return Notifications.Any(); } }

        protected new IActionResult Response(object obj = null, string msg = null)
        {
         
            if (HasNotification)
                return BadRequest( new { success = false, message = msg, data = Notifications });

            return Ok( new { success = true, message = msg, data = obj });
        }

        protected void ErrosModelStateNotification(Notifiable notifiable = null)
        {
            var errosModelState = ModelState.Values.SelectMany(v => v.Errors);
            var erros = errosModelState.Select(e => new Notification("ModelState",e.ErrorMessage)).ToList();
            if (notifiable != null) notifiable.Notifications.ToList().ForEach(n => erros.Add(n));
            erros.ForEach(e => Notifications.Add(e));
        }

        protected void ErrosModelStateNotification(List<Notifiable> notifiables = null)
        {
            var errosModelState = ModelState.Values.SelectMany(v => v.Errors);
            var erros = errosModelState.Select(e => new Notification("ModelState", e.ErrorMessage)).ToList();
            if (notifiables != null && notifiables.Any()) ErrosNotifications(notifiables);
        }

        protected void ErrosNotifications(List<Notifiable> notifiables)
        {
            bool exist = notifiables.Exists(n => !n.Valid);
            if (!exist) return ;

            var notifications = (from notifiable in notifiables
                                 from notification in notifiable.Notifications
                                 select notification).ToList();

            notifications.ForEach(n => Notifications.Add(n));
        }

        protected void AdicionaErroModelState(string key, string value)
        {
            this.Notifications.Add(new Notification(key, value));
        }

    }
}
