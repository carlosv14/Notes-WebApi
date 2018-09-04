namespace Notes.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> All();

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Delete(TEntity entity);

        TEntity Find(params object[] keys);

        Task<TEntity> FindAsync(params object[] keys);

        TEntity First(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TResult> Transform<TResult>(Expression<Func<TEntity, TResult>> predicate);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
