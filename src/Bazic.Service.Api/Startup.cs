using Bazic.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Bazic.Service.Api.JWTConfig;
using Bazic.Service.Api.Configurations;
using Bazic.Service.Api.Middlewares.ErrorException;

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
            IdentityStartupConfig.Config(services);
            PolicyStartupConfig.Config(services);
            JWTStartupConfig.Config(services, Configuration);
           

            services.AddMvc().AddJsonOptions(opt => 
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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
            app.UseErrorExceptionMiddleware();
            app.UseAuthentication();
            
            app.UseMvc();
        }
    }
}
