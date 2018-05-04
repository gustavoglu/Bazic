using Bazic.Domain.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bazic.Domain.Interfaces.Repositorys
{
    public interface IRepository<T> where T : EntityBase
    {
        T Criar(T obj);
        T Atualizar(T obj);
        bool Deletar(Guid id);
        T TrazerPorId(Guid id);
        IEnumerable<T> Pesquisar(Expression<Func<T,bool>> predicate);
        IEnumerable<T> TrazerTodos();
        IEnumerable<T> TrazerTodosAtivos();
        IEnumerable<T> TrazerTodosDeletados();
    }
}
