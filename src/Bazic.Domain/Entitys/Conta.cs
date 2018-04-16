using Bazic.Domain.Core.Entitys;
using System;

namespace Bazic.Domain.Entitys
{
    public class Conta : EntityBase
    {
        public string NomeCompleto { get; set; }
        public Guid Id_contaTipo { get; set; }
    }
}
