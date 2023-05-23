using TT.Business.Events;
using TT.Business.Models;

namespace TT.Business.Interfaces;

public interface IGetClientMethodService
{
    Task<IEnumerable<ContextRequest>> GetClientsMethods(
        (RunSearchEvent searchEvent, CancellationToken cancellationToken) args);
}