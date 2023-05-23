using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TT.Data.Entities;

namespace TT.Data.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasOne(x => x.SearchResult)
            .WithMany(x => x.Routes);
        builder.HasIndex(p => new { p.Origin, p.Destination, p.OriginDateTime })
            .IncludeProperties(p => new { p.DestinationDateTime, p.Price, p.TimeLimit });
    }
}