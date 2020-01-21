using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskApiGateway
{
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.DeclaringType.GetCustomAttributes(true)
           .Union(context.MethodInfo.GetCustomAttributes(true))
           .OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
           .Union(context.MethodInfo.GetCustomAttributes(true))
           .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Security.Add(new OpenApiSecurityRequirement()
            {
              {
                new OpenApiSecurityScheme
                {
                Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    },
                    //Scheme = "oauth2",
                    //Name = "Bearer",
                    //In = ParameterLocation.Header
                },
                new List<string>()
              }
            });

            //if (operation.Parameters == null)
            //    operation.Parameters = new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "Authorization",
            //    In =  ParameterLocation.Header,
            //    Required = true // set to false if this is optional
            //});
        }
    }

}
