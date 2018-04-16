using Bazic.Domain.Core.Entitys;
using System.Collections.Generic;

namespace Bazic.Domain.Entitys
{
    public class ContaTipo : EntityBase
    {
        public string Descricao { get; set; }

        public virtual IEnumerable<Conta> Contas { get; set; }
    }
}
