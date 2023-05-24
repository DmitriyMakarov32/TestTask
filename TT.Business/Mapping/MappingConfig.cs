using JetBrains.Annotations;
using Mapster;
using TT.Business.Models;
using TT.Clients.Base.Models;
using TT.Clients.Base.Models.Request;
using TT.Clients.ProviderOne.Models;
using TT.Clients.ProviderOne.Request;
using TT.Data.Entities;
using TT.Data.Enum;
using Route = TT.Data.Entities.Route;
using SearchRequest = TT.Data.Models.Query.SearchRequest;
using SearchResult = TT.Business.Models.SearchResult;

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

        config.NewConfig<(Route[] routes, Search search), SearchResponse>()
            .Map(dest => dest.Routes, source => source.routes)
            .Map(dest => dest.MaxPrice, source => source.routes.Max(x => x.Price))
            .Map(dest => dest.MinPrice, source => source.routes.Min(x => x.Price))
            .Map(dest => dest.MaxMinutesRoute,
                source => (int)source.routes.Max(x => x.DestinationDateTime - x.OriginDateTime).TotalMinutes)
            .Map(dest => dest.MinMinutesRoute,
                source => (int)source.routes.Min(x => x.DestinationDateTime - x.OriginDateTime).TotalMinutes)
            .Map(dest => dest.SearchState, source => source.search.SearchState);

        config.NewConfig<SearchResult, Data.Entities.SearchResult>()
            .Map(dest => dest.Id, source => Guid.NewGuid())
            .Map(dest => dest.Routes, source => source.Routes)
            .Map(dest => dest.SearchId, source => source.ContextRequest.SearchEvent.Id)
            .Map(dest => dest.SearchResultState,
                source => source.Routes.Any() ? SearchResultStateEnum.Success : SearchResultStateEnum.Error)
            .Map(dest => dest.ProviderId, source => source.ProviderId);

        config.NewConfig<(ContextRequest contextRequest, (TT.Clients.Base.Models.Route[] routes, int providerId) result), SearchResult>()
            .Map(dest => dest.Routes, source => source.result.routes)
            .Map(dest => dest.ProviderId, source => source.result.providerId)
            .Map(dest => dest.ContextRequest, source => source.contextRequest);


    }
}