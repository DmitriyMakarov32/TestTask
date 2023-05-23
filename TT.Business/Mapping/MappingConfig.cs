using JetBrains.Annotations;
using Mapster;
using TT.Clients.Base.Models;
using TT.Clients.Base.Models.Request;
using TT.Clients.ProviderOne.Models;
using TT.Clients.ProviderOne.Request;
using TT.Data.Models.Query;

namespace TT.Business.Mapping;

[UsedImplicitly]
public sealed class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SearchRequest, ClientSearchRequest>()
            .Map(dest => dest.Origin, source => source.Origin)
            .Map(dest => dest.Destination, source => source.Destination)
            .Map(dest => dest.OriginDateTime, source => source.OriginDateTime)
            .AfterMapping((source, dest) =>
            {
                if (source.Filters == null) return;
                dest.MinTimeLimit = source.Filters.MinTimeLimit;
                dest.MaxPrice = source.Filters.MaxPrice;
                dest.DestinationDateTime = source.Filters.DestinationDateTime;
            });
    }
}