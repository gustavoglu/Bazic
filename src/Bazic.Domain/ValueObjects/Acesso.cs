using System.Collections.Generic;

namespace Bazic.Domain.ValueObjects
{
    public class Acesso
    {
        public Acesso(string descricao, IEnumerable<Acesso_Opcao> opcoes)
        {
            Descricao = descricao;
            Opcoes = opcoes;
        }

        public string Descricao { get; set; }
        public IEnumerable<Acesso_Opcao> Opcoes { get; set; }
    }
}
