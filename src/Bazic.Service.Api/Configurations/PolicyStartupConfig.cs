using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Bazic.Service.Api.Configurations
{
    public class PolicyStartupConfig
    {
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
            AdicionaAcesso(opt, "Acessos");
        }

        private static void AdicionaAcesso(AuthorizationOptions opt,string nomeAcesso, List<string> opcoes = null)
        {
            if(opcoes == null || !opcoes.Any())
            {
                opt.AddPolicy(nomeAcesso, pl => pl.RequireClaim("Visualizar"));
                opt.AddPolicy(nomeAcesso, pl => pl.RequireClaim("Inserir"));
                opt.AddPolicy(nomeAcesso, pl => pl.RequireClaim("Editar"));
                opt.AddPolicy(nomeAcesso, pl => pl.RequireClaim("Excluir"));
                return;
            }

            foreach (var opcao in opcoes) opt.AddPolicy(nomeAcesso, pl => pl.RequireClaim(opcao));
        }
    }
}
