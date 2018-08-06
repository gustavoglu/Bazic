using System.Collections.Generic;

namespace Bazic.Domain.Core.Acessos
{
    public static class AcessoList
    {
        public static List<Acesso> Acessos
        {
            get
            {
                return new List<Acesso>
                {
                    new Acesso("Acesso a Tudo",null,false),
                    new Acesso("Acessos"),
                };
            }
        }
    }
}
