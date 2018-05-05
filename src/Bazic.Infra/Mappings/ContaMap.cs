using Bazic.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazic.Infra.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> b)
        {
            b.HasKey(c => c.Id);

            b.HasOne(c => c.ContaTipo)
                .WithMany(ct => ct.Contas)
                .HasForeignKey(c => c.Id_contaTipo)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
