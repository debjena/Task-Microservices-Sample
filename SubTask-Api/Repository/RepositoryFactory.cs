using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubTask_Api.Interfaces;
using SubTask_Api.Model;

namespace SubTask_Api.Repository
{
    public class RepositoryFactory:IRepositoryFactory
    {
        private SubTaskDbContext _repoContext;
        private ISubTaskRepository _task;
        public RepositoryFactory(SubTaskDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public ISubTaskRepository SubTask
        {
            get
            {
                if (_task == null)
                {
                    _task = new SubTaskRepository(_repoContext);
                }

                return _task;
            }
        }
    }
}
