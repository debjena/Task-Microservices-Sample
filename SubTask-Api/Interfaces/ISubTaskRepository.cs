using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubTask_Api.Repository;

namespace SubTask_Api.Interfaces
{
    public interface ISubTaskRepository : IRepository<Model.SubTask>
    {
        Task<IEnumerable<Model.SubTask>> GetAllSubTasksAsync();
        Task<Model.SubTask> GetSubTaskByIdAsync(long taskId);
        Task CreateSubTaskAsync(Model.SubTask task);
        Task DeleteSubtaskAsync(Model.SubTask task);
    }
}
