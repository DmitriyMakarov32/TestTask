using JetBrains.Annotations;
using MassTransit;
using TT.Business.Events;
using TT.Business.Interfaces;
using TT.Business.Models;
using TT.Shared.Helpers;

namespace TT.Business.Consumers;

[UsedImplicitly]
public class RunSearchConsumer : IConsumer<RunSearchEvent>
{
    private readonly IGetClientMethodService _getClientMethodService;
    private readonly ISendClientMethodsService _sendClientMethodsService;
    private readonly ISaveResultService _saveResultService;

    public RunSearchConsumer(IGetClientMethodService getClientMethodService,
        ISendClientMethodsService sendClientMethodsService,
        ISaveResultService saveResultService)
    {
        _getClientMethodService = getClientMethodService;
        _sendClientMethodsService = sendClientMethodsService;
        _saveResultService = saveResultService;
    }

    public async Task Consume(ConsumeContext<RunSearchEvent> context)
    {
        var request = context.Message;

        var dataFlow = new TplHelper<(RunSearchEvent, CancellationToken), SearchResult>();
        dataFlow.AddManyStepAsync<(RunSearchEvent, CancellationToken), ContextRequest>(_getClientMethodService.GetClientsMethods)
            .AddStepAsync<ContextRequest, SearchResult>(_sendClientMethodsService.SendClientsMethodRequest)
            .AddFinalStep<SearchResult>(_saveResultService.SaveResult);

        await dataFlow.Execute((request, context.CancellationToken));
    }
}