using Microsoft.EntityFrameworkCore.Metadata;

namespace TT.Business.Events;

public class RunSearchEvent
{
    public RunSearchEvent(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}