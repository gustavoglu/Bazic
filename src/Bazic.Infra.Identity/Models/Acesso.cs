using System.Collections.Generic;
using System.Linq;

namespace Bazic.Infra.Identity.Models
{
    public class Acesso
    {
        public Acesso(string descricao, IEnumerable<Acesso_Opcao> opcoes = null)
        {
            Descricao = descricao;
            if (opcoes == null || !opcoes.Any())
                Opcoes = new List<Acesso_Opcao>
                {
                    new Acesso_Opcao("Visualizar"),
                    new Acesso_Opcao("Inserir"),
                    new Acesso_Opcao("Editar"),
                    new Acesso_Opcao("Excluir"),
                };
            else
                Opcoes = opcoes;
        }

        public string Descricao { get; set; }
        public IEnumerable<Acesso_Opcao> Opcoes { get; set; }
    }
}
