﻿using Aletheia.Domain.Interfaces;
using Aletheia.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Aletheia.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FIAPDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(FIAPDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
