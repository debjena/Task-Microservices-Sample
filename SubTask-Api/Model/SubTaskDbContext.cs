using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubTask_Api.Model
{
    public class SubTaskDbContext:DbContext
    {
        public SubTaskDbContext(DbContextOptions<SubTaskDbContext> options)
           : base(options)
        {
        }

        public DbSet<SubTask> Tasks { get; set; }
    }
}
