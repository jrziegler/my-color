using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyColor.Infra.IoC.Interfaces;

namespace MyColor.Infra.IoC
{
    public class OptionsBuilderDbInMemory : IOptionsBuilder
    {
        public DbContextOptionsBuilder DefineDb(DbContextOptionsBuilder options, IConfiguration configuration)
        {
            options.UseInMemoryDatabase(configuration.GetConnectionString("DefaultConnection"));
            return options;
        }
    }
}
