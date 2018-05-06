using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bazic.Domain.Core.Notifications;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Bazic.Service.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    {
        public ValuesController(IDomainNotificationHandler<DomainNotification> notifications) : base(notifications)
        {
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get([FromServices]IContaRepository contaRepository)
        {
            return Response (new string[] { "value1", "value2" },"Ok");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
