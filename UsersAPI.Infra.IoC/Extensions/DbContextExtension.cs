using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersAPI.Infra.Data.Contexts;

namespace UsersAPI.Infra.IoC.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDbContextConfig
            (this IServiceCollection services, IConfiguration configuration)
        {
            var databaseProvider = configuration.GetSection("DatabaseProvider").Value;

            switch (databaseProvider)
            {
                case "SqlServer":
                    services.AddDbContext<DataContext>(opt =>
                    {
                        opt.UseSqlServer(configuration.GetConnectionString("BDUsersAPI"));
                    });
                    break;

                case "InMemory":
                    services.AddDbContext<DataContext>(opt =>
                    {
                        opt.UseInMemoryDatabase(databaseName: "BDUsersAPI");
                    });
                    break;
            }
            return services;
        }
    }
}