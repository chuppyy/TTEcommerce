using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public T GetById(string id)
        {
            return _entities.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var entity = await _entities.SingleOrDefaultAsync(e => e.Id == id);
            return entity;
        }
    }
}
