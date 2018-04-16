using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bazic.Domain.Core.Entitys;
using Bazic.Domain.Interfaces.Repositorys;

namespace Bazic.Infra.Data.Repositorys
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public Repository()
        {

        }

        public T Atualizar(T obj)
        {
            throw new NotImplementedException();
        }

        public T Criar(T obj)
        {
            throw new NotImplementedException();
        }

        public bool Deletar(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Pesquisar(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T TrazerPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> TrazerTodos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> TrazerTodosAtivos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> TrazerTodosDeletados()
        {
            throw new NotImplementedException();
        }
    }
}
