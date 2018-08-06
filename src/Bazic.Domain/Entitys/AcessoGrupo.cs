using Bazic.Domain.Core.Entitys;
using System.Collections.Generic;

namespace Bazic.Domain.Entitys
{
    public class AcessoGrupo : EntityBase
    {
        public string Descricao { get; set; }
        public List<AcessoGrupo_Acesso> Acessos { get; set; }
        public ICollection<Conta_AcessoGrupo> Conta_AcessoGrupos  { get; set; }
    }
}
