using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bazic.Service.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        protected new IActionResult Response(object obj = null, string msg = null)
        {
            if (!ModelState.IsValid)
                return BadRequest( new { success = false, message = msg, data = ErrosModelState() });

            return Ok( new { success = true, message = msg, data = obj });
        }

        protected IEnumerable<string> ErrosModelState()
        {
            if (ModelState.IsValid) { return new List<string>(); }
            var errosModelState = ModelState.Values.SelectMany(v => v.Errors);
            var erros = errosModelState.Select(e => e.ErrorMessage);
            return erros;
        }

        protected void AdicionaErroModelState(string key, string value)
        {
            this.ModelState.AddModelError(key, value);
        }

    }
}
