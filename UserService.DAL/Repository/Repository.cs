using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserService.DAL.Context;
using UserService.DAL.Models;

namespace UserService.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;

        private readonly DbSet<T> entities;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            this.entities = this.context.Set<T>();
        }

        public async Task<T> AddOrUpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            if (this.context.Entry(entity).State == EntityState.Detached)
            {
                entity.Created = DateTime.Now;
                this.entities.Add(entity);
            }
            else
            {
                entity.Updated = DateTime.Now;
                this.context.Entry(entity).State = EntityState.Modified;
            }

            await this.context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.entities.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetAsync(long id)
        {
            return await this.entities.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return this.entities.Where(predicate);
        }
    }
}