using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.Intrinsics;

namespace UsersAPI.Services.Extensions
{
    public static class SwaggerDocExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
               new OpenApiInfo
               {
                   Title = "User API - Coti Informatica",
                   Description = " API para controle de usuarios - Formação de Arquiteto",
                   Version = "v1",
                   Contact = new OpenApiContact
                   {
                       Name = "COTI Informatica",
                       Email = "contato@cotiinformatica.com.br",
                       Url = new Uri("http://www.cotiinformatica.com.br")
                   }
               });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);

            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "UsersAPI")
            );

            return app;
        }
    }
}
