using TT.Clients.ProviderTwo.Models;

namespace TT.Clients.ProviderTwo.Response;

public class ProviderTwoSearchResponse
{
    // Mandatory
    // Array of routes
    public ProviderTwoRoute[] Routes { get; set; }
}