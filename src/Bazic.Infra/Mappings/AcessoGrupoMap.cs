using Bazic.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Bazic.Infra.Data.Mappings
{
    public class AcessoGrupoMap : IEntityTypeConfiguration<AcessoGrupo>
    {
        public void Configure(EntityTypeBuilder<AcessoGrupo> b)
        {
        }
    }
}
