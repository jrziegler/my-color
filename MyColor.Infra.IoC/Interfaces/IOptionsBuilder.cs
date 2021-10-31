using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyColor.Infra.IoC.Interfaces
{
    public interface IOptionsBuilder
    {
        DbContextOptionsBuilder DefineDb(DbContextOptionsBuilder options, IConfiguration configuration);
    }
}
