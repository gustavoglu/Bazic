using Bazic.Infra.Identity.Models;
using System.Collections.Generic;

namespace Bazic.Infra.Identity.Acessos
{
    public class AcessosList
    {
        public static List<Acesso> Acessos
        {
            get
            {
                return new List<Acesso>
                {
                    new Acesso("Admin",new List<Acesso_Opcao>{ new Acesso_Opcao("Admin") }),
                    new Acesso("Acessos"),
                    new Acesso("Produtos"),
                    new Acesso("ProdutoCategorias"),
                };
            }
        }

    }
}
