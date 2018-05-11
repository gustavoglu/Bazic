using Bazic.Infra.Identity.Acessos;
using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Bazic.Service.Api.Configurations
{
    public class PolicyStartupConfig
    {
        private static List<Acesso> Acessos { get { return AcessosList.Acessos; } }

        public static void Config(IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

                AdicionaPolicys(auth);
            });
        }

        private static void AdicionaPolicys(AuthorizationOptions opt)
        {
            Acessos.ForEach(a => a.Opcoes.ToList()
                                  .ForEach( o => opt.AddPolicy($"{o.Descricao}{o.Descricao}", 
                                                               plc => plc.RequireClaim(a.Descricao,o.Descricao))));
        }
    }
}
