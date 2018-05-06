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

        public void Atualizar(T obj)
        {
            dbSet.Update(obj);
        }

        public async void Criar(T obj)
        {
            await dbSet.AddAsync(obj);
        }

        public async void Deletar(Guid id)
        {
            var entity = await TrazerPorId(id);
            dbSet.Remove(entity);
        }

        public IEnumerable<T> Pesquisar(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public async Task<T> TrazerPorId(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public IEnumerable<T> TrazerTodos()
        {
            return dbSet;
        }

        public IEnumerable<T> TrazerTodosAtivos()
        {
            return dbSet.Where(e => !e.Deletado);
        }

        public IEnumerable<T> TrazerTodosDeletados()
        {
            return dbSet.Where(e => e.Deletado);
        }

        public async void CriarVarios(List<T> objs)
        {
            await dbSet.AddRangeAsync(objs);
        }

        public void DeletarVarios(List<T> objs)
        {
            dbSet.RemoveRange(objs);
        }
    }
}
