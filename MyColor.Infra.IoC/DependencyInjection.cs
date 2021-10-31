using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyColor.Application.Interfaces;
using MyColor.Application.Mappings;
using MyColor.Application.Services;
using MyColor.Domain.Interfaces;
using MyColor.Infra.Data.Context;
using MyColor.Infra.Data.Csv;
using MyColor.Infra.Data.Interfaces;
using MyColor.Infra.Data.Mappings;
using MyColor.Infra.Data.Repositories;
using MyColor.Infra.Logging.Services;

namespace MyColor.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFileReader, CsvFileReader>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            services.AddAutoMapper(typeof(CsvToPersonFromCsvMappingProfile));

            return services;
        }
    }
}
