using Bazic.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazic.Infra.Data.Mappings
{
    public class AcessoGrupo_AcessoMap : IEntityTypeConfiguration<AcessoGrupo_Acesso>
    {
        public void Configure(EntityTypeBuilder<AcessoGrupo_Acesso> b)
        {
            b.HasOne(aga => aga.AcessoGrupo)
                .WithMany(ag => ag.Acessos)
                .HasForeignKey(aga => aga.Id_acessoGrupo)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
