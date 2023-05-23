using JetBrains.Annotations;
using Mapster;
using TT.Clients.Base.Models;
using TT.Clients.Base.Models.Request;
using TT.Clients.ProviderTwo.Models;
using TT.Clients.ProviderTwo.Request;

namespace TT.Clients.ProviderTwo.Mapping;

[UsedImplicitly]
public sealed class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClientSearchRequest, ProviderTwoSearchRequest>()
            .Map(dest => dest.Departure, source => source.Origin)
            .Map(dest => dest.Arrival, source => source.Destination)
            .Map(dest => dest.DepartureDate, source => source.OriginDateTime)
            .Map(dest => dest.MinTimeLimit, source => source.MinTimeLimit);



        config.NewConfig<ProviderTwoRoute, Route>()
            .Map(dest => dest.Id, source => Guid.NewGuid())
            .Map(dest => dest.Origin, source => source.Departure.Point)
            .Map(dest => dest.Destination, source => source.Arrival.Point)
            .Map(dest => dest.OriginDateTime, source => source.Departure.Date)
            .Map(dest => dest.DestinationDateTime, source => source.Arrival.Date)
            .Map(dest => dest.Price, source => source.Price)
            .Map(dest => dest.TimeLimit, source => source.TimeLimit);
    }
}