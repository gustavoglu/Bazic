using Bazic.Domain.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bazic.Domain.Interfaces.Repositorys
{
    public interface IRepository<T> where T : EntityBase
    {
        void Criar(T obj);
        void CriarVarios(List<T> objs);
        void Atualizar(T obj);
        void Deletar(Guid id);
        void DeletarVarios(List<T> objs);
        Task<T> TrazerPorId(Guid id);
        IEnumerable<T> Pesquisar(Expression<Func<T,bool>> predicate);
        IEnumerable<T> TrazerTodos();
        IEnumerable<T> TrazerTodosAtivos();
        IEnumerable<T> TrazerTodosDeletados();
    }
}
