
using Microsoft.EntityFrameworkCore;
using QulixPhotoStock.Data.Contexts;
using QulixPhotoStock.Data.IRepositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
    {
        private readonly QulixPhotoStockDbContext dbContext;
        private readonly DbSet<TSource> dbSet;

        public GenericRepository()
        {
            dbContext = new QulixPhotoStockDbContext();
            dbSet = dbContext.Set<TSource>();
        }

        public async Task<TSource> CreateAsync(TSource source)
        {
            var entity = (await dbSet.AddAsync(source)).Entity;

            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<TSource, bool>> predicate)
        {
            var entity = await GetAsync(predicate);

            if (entity is null)
                return false;

            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> predicate = null)
            => predicate is null ? dbSet : dbSet.Where(predicate);

        public virtual async Task<TSource> GetAsync(Expression<Func<TSource, bool>> predicate)
            => await dbSet.FirstOrDefaultAsync(predicate);

        public async Task<TSource> UpdateAsync(TSource source)
        {
            var entity = dbSet.Update(source).Entity;

            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
