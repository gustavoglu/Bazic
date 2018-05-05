using Bazic.Domain.Core.Entitys;
using Bazic.Domain.Entitys;
using Bazic.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bazic.Infra.Data.Context
{
    public class BazicContext : DbContext
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<ContaTipo> ContaTipos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ContaMap());
            modelBuilder.ApplyConfiguration(new ContaTipoMap());
        }

        public override int SaveChanges()
        {
            var adicionados = ChangeTracker.Entries().Where(e => e.Entity is EntityBase && e.State == EntityState.Added).ToList();
            var atualizados = ChangeTracker.Entries().Where(e => e.Entity is EntityBase && e.State == EntityState.Modified).ToList();
            var deletados = ChangeTracker.Entries().Where(e => e.Entity is EntityBase && e.State == EntityState.Deleted).ToList();

            if (adicionados.Any())
                AdicionaEntitys(adicionados);

            if (atualizados.Any())
                AtualizaEntitys(atualizados);

            if (adicionados.Any())
                DeletaEntitys(deletados);

            return base.SaveChanges();
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

        private void AdicionaEntitys(List<EntityEntry> adicionados)
        {
            foreach (var adicionado in adicionados)
            {
                var entity = (EntityBase)adicionado.Entity;
                entity.CriadoEm = DateTime.Now;
            }
        }
        private void AtualizaEntitys(List<EntityEntry> atualizados)
        {
            foreach (var atualizado in atualizados)
            {
                var entity = (EntityBase)atualizado.Entity;
                atualizado.Property("CriadoEm").IsModified = false;
                atualizado.Property("CriadoPor").IsModified = false;
                atualizado.Property("DeletadoEm").IsModified = false;
                atualizado.Property("DeletadoPor").IsModified = false;

                entity.AtualizadoEm = DateTime.Now;
            }
        }
        private void DeletaEntitys(List<EntityEntry> deletados)
        {
            foreach (var deletado in deletados)
            {
                deletado.State = EntityState.Modified;
                deletado.Property("CriadoEm").IsModified = false;
                deletado.Property("CriadoPor").IsModified = false;
                deletado.Property("AtualizadoEm").IsModified = false;
                deletado.Property("AtualizadoPor").IsModified = false;
                var entity = (EntityBase)deletado.Entity;
                
                entity.DeletadoEm = DateTime.Now;
            }
        }
    }
}
