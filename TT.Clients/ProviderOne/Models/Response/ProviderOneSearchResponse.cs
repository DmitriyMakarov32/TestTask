using TT.Clients.ProviderOne.Models;

namespace TT.Clients.ProviderOne.Response;

public class ProviderOneSearchResponse
{
    // Mandatory
    // Array of routes
    public ProviderOneRoute[] Routes { get; set; }
}