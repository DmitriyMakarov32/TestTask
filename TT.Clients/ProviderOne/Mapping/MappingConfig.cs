using JetBrains.Annotations;
using Mapster;
using TT.Clients.Base.Models;
using TT.Clients.Base.Models.Request;
using TT.Clients.ProviderOne.Models;
using TT.Clients.ProviderOne.Request;

namespace TT.Clients.ProviderOne.Mapping;

[UsedImplicitly]
public sealed class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClientSearchRequest, ProviderOneSearchRequest>()
            .Map(dest => dest.From, source => source.Origin)
            .Map(dest => dest.To, source => source.Destination)
            .Map(dest => dest.DateFrom, source => source.OriginDateTime)
            .Map(dest => dest.DateTo, source => source.DestinationDateTime);

        config.NewConfig<ProviderOneRoute, Route>()
            .Map(dest => dest.Id, source => Guid.NewGuid())
            .Map(dest => dest.Origin, source => source.From)
            .Map(dest => dest.Destination, source => source.To)
            .Map(dest => dest.OriginDateTime, source => source.DateFrom)
            .Map(dest => dest.DestinationDateTime, source => source.DateTo)
            .Map(dest => dest.Price, source => source.Price)
            .Map(dest => dest.TimeLimit, source => source.TimeLimit);


    }
}