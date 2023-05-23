using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TT.Data.DbContexts;

namespace TT.Data;

public static class DependencyInjection
{
    public static void AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TestTaskDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("postgresConnection"), b =>
            {
                b.MigrationsHistoryTable("__EFMigrationsHistory", "test_task");
            }));
    }
}