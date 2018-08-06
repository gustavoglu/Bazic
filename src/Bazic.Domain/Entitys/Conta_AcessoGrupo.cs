using Bazic.Domain.Core.Entitys;
using System;

namespace Bazic.Domain.Entitys
{
    public class Conta_AcessoGrupo : EntityBase
    {
        public Guid Id_conta { get; set; }
        public Guid Id_acessoGrupo { get; set; }

        public Conta Conta { get; set; }
        public AcessoGrupo AcessoGrupo { get; set; }
    }
}
