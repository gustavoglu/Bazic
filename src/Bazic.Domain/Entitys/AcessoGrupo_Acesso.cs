using Bazic.Domain.Core.Entitys;
using System;

namespace Bazic.Domain.Entitys
{
    public class AcessoGrupo_Acesso : EntityBase
    {
        public string Tipo { get; set; }
        public string Acesso { get; set; }
        public Guid Id_acessoGrupo { get; set; }
        public AcessoGrupo AcessoGrupo { get; set; }
    }
}
