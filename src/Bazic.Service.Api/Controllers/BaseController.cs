using Bazic.Domain.Core.Notifications;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bazic.Service.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        protected BaseController(IDomainNotificationHandler<DomainNotification> notifications)
        {
            _notifications = notifications;
        }

        protected bool HasNotification { get { return _notifications.HasNotification(); } }

        protected new IActionResult Response(object obj = null, string msg = null)
        {
            AddErrosModelStateInNotifications();

            if (HasNotification)
                return BadRequest(new { success = false, message = msg, data = ErrosNotifications() });

            return Ok(new { success = true, message = msg, data = obj });
        }

        protected void AddErrosModelStateInNotifications()
        {
            var errosModelState = ModelState.Values.SelectMany(v => v.Errors);
            var erros = errosModelState.Select(e => new DomainNotification("ModelState", e.ErrorMessage)).ToList();
            erros.ForEach(e => _notifications.Handler(e));
        }

        protected void AddErroInNotifications(string key, string value)
        {
            this._notifications.Handler(new DomainNotification(key, value));
        }

        protected void AddErrosNotifiableInNotifications(Notifiable notifiable)
        {
            if (notifiable.Valid) return;
            notifiable.Notifications.ToList().ForEach(n => _notifications.Handler(new DomainNotification(n.Property, n.Message)));
        }

        protected object ErrosNotifications()
        {
            return _notifications.GetNotifications().Select(n => new { Key = n.Key, Value = n.Value }).ToList();
        }

    }
}
