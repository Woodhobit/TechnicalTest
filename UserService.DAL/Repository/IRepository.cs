using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserService.DAL.Models;

namespace UserService.DAL.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> AddOrUpdateAsync(T entity);

        Task<T> GetAsync(long id);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        Task SaveChangesAsync();
    }
}
