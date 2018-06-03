using System;
using System.Collections.Generic;
using System.Linq;

namespace Bazic.Infra.Identity.Models
{
    public class Acesso
    {
        private List<Acesso_Opcao> OpcoesPadrao
        {
            get
            {
                return new List<Acesso_Opcao>{
                    new Acesso_Opcao("Visualizar"),
                    new Acesso_Opcao("Inserir"),
                    new Acesso_Opcao("Editar"),
                    new Acesso_Opcao("Excluir"),
                };
            }
        }

        public Acesso(string descricao, IEnumerable<Acesso_Opcao> opcoes = null, bool addAcessosPadrao = false)
        {
            Descricao = descricao;
            if (opcoes == null || !opcoes.Any()) Opcoes = OpcoesPadrao;
            else
            {
                if (addAcessosPadrao)
                {
                    Opcoes = OpcoesPadrao;
                    opcoes.ToList().ForEach(o => Opcoes.Add(o));
                }
                else
                {
                    Opcoes = opcoes.ToList();
                }
            }
        }

        public string Descricao { get; set; }
        public List<Acesso_Opcao> Opcoes { get; set; }
    }
}
