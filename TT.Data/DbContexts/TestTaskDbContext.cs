using Microsoft.EntityFrameworkCore;
using TT.Data.Entities;

namespace TT.Data.DbContexts;

public class TestTaskDbContext : DbContext
{
    public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : base(options)
    {}

    public DbSet<Search> Searches => Set<Search>();
    public DbSet<Query> Queries => Set<Query>();
    public DbSet<SearchResult> SearchResults => Set<SearchResult>();
    public DbSet<Route> Routes => Set<Route>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("test_task");
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(TestTaskDbContext).Assembly);
    }

}