
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Univerus.Console.Domain.Config;
using Univerus.Console.Application.Services;
using Univerus.Console.Infrastructure.Services;
using Univerus.Console.Shared.DatabaseAccess;

namespace Univerus.Console.Infrastructure.Extension
{
    public static class AppDependencyResolver
    {
        public static void ConfigureUniverusCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionStrings connectionString = new ConnectionStrings();
            configuration.GetSection("ConnectionStrings").Bind(connectionString);
            services.AddSingleton(connectionString);

            services.AddScoped<IDALBase, DALBase>();

            services.AddScoped<IPersonServices, PersonServices>();
            services.AddScoped<IPersonTypeServices, PersonTypeServices>();

        }
    }
}
