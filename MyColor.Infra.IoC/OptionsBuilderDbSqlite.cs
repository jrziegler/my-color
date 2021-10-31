using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyColor.Infra.IoC.Interfaces;

namespace MyColor.Infra.IoC
{
    public class OptionsBuilderDbSqlite : IOptionsBuilder
    {
        public DbContextOptionsBuilder DefineDb(DbContextOptionsBuilder options, IConfiguration configuration)
        {
            options.UseSqlite(configuration.GetConnectionString("SQLiteConnection"));
            return options;
        }
    }
}
