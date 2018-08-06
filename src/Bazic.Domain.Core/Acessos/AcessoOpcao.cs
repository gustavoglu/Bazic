using Bazic.Domain.Core.ValueObjects;

namespace Bazic.Domain.Core.Acessos
{
    public class AcessoOpcao : ValueObject
    {
        public AcessoOpcao(string descricao, bool? concedido = false)
        {
            Descricao = descricao;
            Concedido = concedido.Value;
        }

        public string Descricao { get; set; }
        public bool Concedido { get; set; } = false;

        public void ConcederAcesso(bool conceder)
        {
            this.Concedido = conceder;
        }
    }
}
