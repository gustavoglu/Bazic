﻿using Bazic.Domain.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bazic.Service.Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    {
        public ValuesController(IDomainNotificationHandler<DomainNotification> notifications) : base(notifications)
        {
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
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
