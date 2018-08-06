using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bazic.Domain.Core.Entitys;
using Bazic.Domain.Interfaces.Repositorys;
using Bazic.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bazic.Infra.Data.Repositorys
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected BazicContext _context { get; private set; }
        protected DbSet<T> dbSet { get; private set; }

        public Repository(BazicContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public virtual void Atualizar(T obj)
        {
            dbSet.Update(obj);
        }

        public virtual async void Criar(T obj)
        {
            await dbSet.AddAsync(obj);
        }

        public virtual async void Deletar(Guid id)
        {
            var entity = await TrazerPorId(id);
            dbSet.Remove(entity);
        }

        public virtual IEnumerable<T> Pesquisar(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public virtual async Task<T> TrazerPorId(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IEnumerable<T> TrazerTodos()
        {
            return dbSet;
        }

        public virtual IEnumerable<T> TrazerTodosAtivos()
        {
            return dbSet.Where(e => !e.Deletado);
        }

        public virtual IEnumerable<T> TrazerTodosDeletados()
        {
            return dbSet.Where(e => e.Deletado);
        }

        public virtual async void CriarVarios(List<T> objs)
        {
            await dbSet.AddRangeAsync(objs);
        }

        public virtual void DeletarVarios(List<T> objs)
        {
            dbSet.RemoveRange(objs);
        }
    }
}
