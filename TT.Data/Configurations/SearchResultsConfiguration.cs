using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TT.Data.Entities;
using TT.Data.Extensions;

namespace TT.Data.Configurations;

public class SearchResultsConfiguration : IEntityTypeConfiguration<SearchResult>
{
    public void Configure(EntityTypeBuilder<SearchResult> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.SearchResultState).HasConversion<string>();
    }
}