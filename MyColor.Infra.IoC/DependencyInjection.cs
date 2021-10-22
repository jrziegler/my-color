using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyColor.Domain.Interfaces;
using MyColor.Infra.Data.Context;
using MyColor.Infra.Data.Repositories;

namespace MyColor.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;

            // TODO:
            // in file appsettings add:
            //"ConnectionStrings": {
            //  "DefaultConnection": "DbInMemory"
            //}
            //
            // register the service in the startup class
            // services.AddInfrastructure(Configuration);
        }
    }
}
