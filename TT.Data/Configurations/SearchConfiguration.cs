using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TT.Data.Entities;

namespace TT.Data.Configurations;

public class SearchConfiguration : IEntityTypeConfiguration<Search>
{
    public void Configure(EntityTypeBuilder<Search> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.SearchState).HasConversion<string>();
        builder.HasMany(p => p.SearchResults)
            .WithOne(p => p.Search);
        builder.HasOne(p => p.Query)
            .WithOne(p => p.Search);
    }
}