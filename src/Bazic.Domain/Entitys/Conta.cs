using Bazic.Domain.Core.Entitys;
using System;
using System.Collections.Generic;

namespace Bazic.Domain.Entitys
{
    public class Conta : EntityBase
    {
        public string NomeCompleto { get; set; }
        public Guid Id_contaTipo { get; set; }
        public bool Admin { get; set; } = false;
        public virtual ContaTipo ContaTipo { get; set; }
        public ICollection<Conta_AcessoGrupo> Conta_AcessoGrupos { get; set; }

    }
}
