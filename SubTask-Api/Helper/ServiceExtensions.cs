using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubTask_Api.Interfaces;
using SubTask_Api.Model;
using SubTask_Api.Repository;

namespace SubTask_Api
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .Build());
            });
        }

      
        public static void ConfigureLoggerService(this IServiceCollection services)
        {

        }

        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SubTaskDbContext>(opt => opt.UseInMemoryDatabase("TaskList"));
            //var connectionString = config["connectionString"];
            //services.AddDbContext<TaskDbContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryFactory(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        }
    }
}
