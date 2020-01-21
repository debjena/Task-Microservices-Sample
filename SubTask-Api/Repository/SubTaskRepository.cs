using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SubTask_Api.Interfaces;
using SubTask_Api.Model;

namespace SubTask_Api.Repository
{
    public class SubTaskRepository : Repository<Model.SubTask>, ISubTaskRepository
    {
        public SubTaskRepository(SubTaskDbContext repositoryContext)
          : base(repositoryContext)
        {
            if (repositoryContext.Tasks.Count() == 0)
            {
                repositoryContext.Tasks.Add(new Model.SubTask { SubTaskDesc = "SubTask1" });
                repositoryContext.SaveChanges();
            }
        }

        public async System.Threading.Tasks.Task CreateSubTaskAsync(Model.SubTask task)
        {
            Create(task);
            await SaveAsync();
        }

        public async  System.Threading.Tasks.Task DeleteSubtaskAsync(Model.SubTask task)
        {
            Delete(task);
            await SaveAsync();
        }

        public async Task<IEnumerable<Model.SubTask>> GetAllSubTasksAsync()
        {
            return await FindAll()
              .OrderBy(x => x.SubTaskDesc)
              .ToListAsync();
        }

        public async Task<Model.SubTask> GetSubTaskByIdAsync(long taskId)
        {
            return await FindByCondition(o => o.Id.Equals(taskId))
                .DefaultIfEmpty()
                .SingleAsync();
        }
    }
}
