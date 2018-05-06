using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bazic.Infra.Identity.Context;
using Bazic.Infra.Identity.Models;
using Bazic.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bazic.Service.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Usuario, IdentityRole>(opc =>
               {
                   opc.Password.RequireDigit = false;
                   opc.Password.RequiredLength = 6;
                   opc.Password.RequireLowercase = false;
                   opc.Password.RequireUppercase = false;
                   opc.Password.RequireNonAlphanumeric = false;
               })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ContextIdentity>();

            services.AddMvc()
                .AddJsonOptions(options => 
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            NativeInjection.Configure(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
