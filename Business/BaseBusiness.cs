using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public abstract class BaseBusiness<T> : IDisposable where T : class
    {
        protected readonly ExpressDbContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly bool _contextOwned;

        protected BaseBusiness(ExpressDbContext context)
        {
            _context = context;
            _contextOwned = false;
            _dbSet = _context.Set<T>();
        }

        protected BaseBusiness()
        {
            _context = new ExpressDbContext();
            _contextOwned = true;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            var existing = await _dbSet.FindAsync(
                _context.Entry(entity).Property("Id").CurrentValue
            );

            if (existing == null)
            { return; }

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            if (_contextOwned)
            {
                _context.Dispose();
            }

        }
    }
}
