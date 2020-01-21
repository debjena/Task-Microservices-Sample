using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Repo_Pattern.Model
{
    public class TasksVM
    {
        public long Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDate { get; set; }
        public bool IsTaskDone { get; set; }
        public string SubTaskDesc { get; set; }
    }
}
