using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.IO;

namespace IntelligentCheckout.Backend.Setup
{
    internal static class Swagger
    {
        public static void Add(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Intelligent Checkout",
                    Version = "v1",
                    Description = "APIs - Intelligent Checkout",
                    Contact = new OpenApiContact
                    {
                        Name = "DevelopersBr",
                        Url = new Uri("https://developers-br.visualstudio.com/IntelligentCheckout")
                    }
                });
                var commentFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var commentFilePath = Path.Combine(AppContext.BaseDirectory, commentFileName);
                options.IncludeXmlComments(commentFilePath);
                options.CustomSchemaIds(x => x.ToString().Replace("`1", string.Empty).Replace("[", "<").Replace("]", ">"));
            });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs - Intelligent Checkout");
            });
        }
    }
}
