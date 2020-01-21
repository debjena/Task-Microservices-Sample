using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SubTask_Api.Model;

namespace SubTask_Api.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected SubTaskDbContext RepositoryContext { get; set; }

        public Repository(SubTaskDbContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
                .Where(expression);
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async System.Threading.Tasks.Task SaveAsync()
        {
            await this.RepositoryContext.SaveChangesAsync();
        }

    }
}
