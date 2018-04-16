using Bazic.Domain.Core.ValueObjects;

namespace Bazic.Domain.ValueObjects
{
    public class Acesso_Opcao : ValueObject
    {
        public Acesso_Opcao(string descricao, bool concedido)
        {
            Descricao = descricao;
            Concedido = concedido;
        }

        public string Descricao { get; set; }
        public bool Concedido { get; set; } = false;
    }
}
