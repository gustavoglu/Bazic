﻿using Bazic.Application.Interfaces;
using Bazic.Application.Services;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Infra.Data.Context;
using Bazic.Infra.Data.Repositorys;
using Bazic.Infra.Identity.Context;
using Bazic.Infra.Identity.Interfaces;
using Bazic.Infra.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bazic.Infra.IoC
{
    public class NativeInjection
    {
        public static void Configure(IServiceCollection service)
        {
            //DATA
            service.AddScoped<BazicContext>();
            service.AddScoped<ContextIdentity>();

            //REPOSITORY
            service.AddScoped<IContaRepository, ContaRepository>();
            service.AddScoped<IContaTipoRepository, ContaTipoRepository>();

            //SERVICES
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IContaService, ContaService>();
        }
    }
}
