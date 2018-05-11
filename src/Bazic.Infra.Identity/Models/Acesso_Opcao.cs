using Bazic.Domain.Core.ValueObjects;

namespace Bazic.Infra.Identity.Models
{
    public class Acesso_Opcao : ValueObject
    {
        public Acesso_Opcao(string descricao, bool? concedido = null)
        {
            Descricao = descricao;
            Concedido = concedido.HasValue ? concedido.Value : false;
        }

        public string Descricao { get; set; }
        public bool Concedido { get; set; } = false;

        public void ConcederAcesso()
        {
            this.Concedido = true;
        }

        public void RemoverAcesso()
        {
            this.Concedido = false;
        }
    }
}
