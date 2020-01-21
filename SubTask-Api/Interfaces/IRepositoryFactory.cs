using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubTask_Api.Interfaces
{
    public interface IRepositoryFactory
    {
        ISubTaskRepository SubTask { get; }
    }
}
