using Bazic.Infra.Identity.Context;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Bazic.Service.Api.Configurations
{
    public class IdentityStartupConfig
    {
        public static void Config(IServiceCollection services)
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
        }
    }
}
