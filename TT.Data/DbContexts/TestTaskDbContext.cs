using Microsoft.EntityFrameworkCore;
using TT.Data.Entities;

namespace TT.Data.DbContexts;

public class TestTaskDbContext : DbContext
{
    public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : base(options)
    {}

    public DbSet<Search> Searches { get; set; }
    public DbSet<Query> Queries { get; set; }
    public DbSet<SearchResult> SearchResults { get; set; }
    public DbSet<Route> Routes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("test_task");
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(TestTaskDbContext).Assembly);
    }

}