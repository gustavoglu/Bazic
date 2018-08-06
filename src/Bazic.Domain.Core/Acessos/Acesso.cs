using Bazic.Domain.Core.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Bazic.Domain.Core.Acessos
{
    public class Acesso : ValueObject
    {
        private List<AcessoOpcao> OpcoesPadrao
        {
            get
            {
                return new List<AcessoOpcao>{
                    new AcessoOpcao("Visualizar"),
                    new AcessoOpcao("Inserir"),
                    new AcessoOpcao("Editar"),
                    new AcessoOpcao("Excluir"),
                };
            }
        }

        public Acesso(string descricao, List<AcessoOpcao> opcoes = null, bool? addAcessosPadrao = true)
        {
            Descricao = descricao;
            Opcoes = new List<AcessoOpcao>();
            if ((opcoes == null || !opcoes.Any()) && !addAcessosPadrao.Value) opcoes.Add(new AcessoOpcao(descricao));
            if (addAcessosPadrao.Value) Opcoes.AddRange(OpcoesPadrao);
            if (opcoes != null && opcoes.Any()) Opcoes.AddRange(opcoes);
        }

        public string Descricao { get; set; }
        public List<AcessoOpcao> Opcoes { get; set; }
    }
}
