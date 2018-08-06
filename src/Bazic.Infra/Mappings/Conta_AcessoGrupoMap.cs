using Bazic.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazic.Infra.Data.Mappings
{
    public class Conta_AcessoGrupoMap : IEntityTypeConfiguration<Conta_AcessoGrupo>
    {
        public void Configure(EntityTypeBuilder<Conta_AcessoGrupo> b)
        {
            b.HasOne(cag => cag.Conta)
                .WithMany(c => c.Conta_AcessoGrupos)
                .HasForeignKey(cag => cag.Id_conta)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(cag => cag.AcessoGrupo)
                .WithMany(c => c.Conta_AcessoGrupos)
                .HasForeignKey(cag => cag.Id_acessoGrupo)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
