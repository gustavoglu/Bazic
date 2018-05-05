using Bazic.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Bazic.Infra.Identity.Context
{
    public class ContextIdentity : IdentityDbContext
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Usuario>().ToTable("Usuarios");
            builder.Entity<IdentityUser>().ToTable("Usuarios");
            builder.Entity<IdentityRole>().ToTable("Regras");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsuarioRegras");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UsuarioClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UsuarioLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RegraClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UsuarioTokens");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connDev = builder.GetSection("ConnectionStrings")
                                 .GetSection("dev")
                                 .Value;

            base.OnConfiguring(optionsBuilder.UseSqlServer(connDev));
        }
    }
}
