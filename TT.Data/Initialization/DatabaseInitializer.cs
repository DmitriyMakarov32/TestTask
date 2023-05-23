using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TT.Data.DbContexts;

namespace TT.Data.Initialization;

public class DatabaseInitializer
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetService<TestTaskDbContext>();
        if (context!.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}