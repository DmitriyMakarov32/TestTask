using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TT.Data.Entities;
using TT.Data.Extensions;

namespace TT.Data.Configurations;

public class QueryConfiguration : IEntityTypeConfiguration<Query>
{
    public void Configure(EntityTypeBuilder<Query> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Data).HasJsonConversion();
    }
}