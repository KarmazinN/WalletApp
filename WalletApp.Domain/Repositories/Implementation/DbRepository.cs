﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WalletApp.Domain.Repositories.Interface;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Domain.DataBase;
using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.DbModels.Interfaces;
using System.Xml.Linq;

namespace WalletApp.Domain.Repositories.Implementation
{
    public class DbRepository<T> : IDbRepository<T> where T : class
    {
        private readonly ApplicationsContext _context;
        private readonly DbSet<T> _dbSet;

        public DbRepository(ApplicationsContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<List<T>> GetListAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool isTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return isTracking ? await orderBy(query).ToListAsync() : await orderBy(query).AsNoTracking().ToListAsync();
            }

            return isTracking ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool isTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return isTracking
                    ? await orderBy(query).FirstOrDefaultAsync()
                    : await orderBy(query).AsNoTracking().FirstOrDefaultAsync();
            }

            return isTracking
                ? await query.FirstOrDefaultAsync()
                : await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T? entityToDelete)
        {
            if (entityToDelete == null) return;

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public DbSet<T> CustomQuery()
        {
            return _dbSet;
        }
    }
}
