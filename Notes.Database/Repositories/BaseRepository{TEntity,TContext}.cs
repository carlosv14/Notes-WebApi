namespace Notes.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class BaseRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected BaseRepository(TContext context)
        {
            this.Context = context;
        }

        public virtual TContext Context { get; protected set; }

        public abstract IQueryable<TEntity> All();

        public TEntity Create(TEntity entity)
        {
            var entry = this.Context.Entry<TEntity>(entity);
            entry.State = EntityState.Added;
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Deleted;
            return entity;
        }

        public TEntity Find(params object[] keys)
        {
            return ((DbSet<TEntity>)this.All()).Find(keys);
        }

        public Task<TEntity> FindAsync(params object[] keys)
        {
            return this.FindAsync(CancellationToken.None, keys);
        }

        public virtual Task<TEntity> FindAsync(CancellationToken token, params object[] keys)
        {
            return ((DbSet<TEntity>)this.All()).FindAsync(token, keys);
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Filter(predicate).First();
        }

        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Filter(predicate).FirstAsync();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.All().Where(predicate).FirstOrDefault();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.All().Where(predicate).FirstOrDefaultAsync();
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.Context.SaveChangesAsync();
        }

        public virtual IQueryable<TResult> Transform<TResult>(Expression<Func<TEntity, TResult>> predicate)
        {
            return this.All().Select(predicate);
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return this.All().Where(predicate);
        }

        public TEntity Update(TEntity entity)
        {

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
            return entity;
        }
    }
}
