using Bazic.Domain.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bazic.Domain.Interfaces.Repositorys
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> Criar(T obj);
        Task<IEnumerable<T>> CriarVarios(List<T> objs);
        Task<T> Atualizar(T obj);
        Task<T> Deletar(Guid id);
        Task<bool> DeletarVarios(List<T> objs);
        Task<T> TrazerPorId(Guid id);
        IEnumerable<T> Pesquisar(Expression<Func<T,bool>> predicate);
        IEnumerable<T> TrazerTodos();
        IEnumerable<T> TrazerTodosAtivos();
        IEnumerable<T> TrazerTodosDeletados();
    }
}
