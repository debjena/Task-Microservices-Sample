using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskApiGateway
{
    public interface iapiaggregator
    {
        Task<List<TaskData>> GetTasks();
    }
    public class apiaggregator:iapiaggregator
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _config;
        public apiaggregator(HttpClient httpClient, IConfiguration config)
        {
            _apiClient = httpClient;
            _config = config;
        }
        public async Task<List<TaskData>> GetTasks()
        {
            //assume fk is the tk id
            var data = await _apiClient.GetStringAsync(_config["taskapi"] + "/api/tasks");
            var tk = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<List<TasksVM>>(data) : null;
            List<TaskData> l = new List<TaskData>();
            foreach(var o in tk)
            {
                var sdata = await _apiClient.GetStringAsync(_config["subtaskapi"] + "/api/subtasks/" + o.Id);
                var stk = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<SubTasksVM>(sdata) : null;
                l.Add(new TaskData() { Id = o.Id, TaskName = o.TaskName, TaskDate = o.TaskDate, IsTaskDone = o.IsTaskDone, SubTask = stk });
            }
            return l;
        }
    }
    public class TasksVM
    {
        public long Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDate { get; set; }
        public bool IsTaskDone { get; set; }
    }
    public class SubTasksVM
    {
        public long Id { get; set; }
        public string SubTaskDesc { get; set; }
    }
    public class TaskData
    {
        public long Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDate { get; set; }
        public bool IsTaskDone { get; set; }
        public SubTasksVM SubTask { get; set; }
    }
}
