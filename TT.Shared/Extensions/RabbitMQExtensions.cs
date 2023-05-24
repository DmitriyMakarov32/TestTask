using TT.Shared.Settings;

namespace TT.Shared.Extensions;

public static class RabbitMQExtensions
{
    public static string GetConnectionString(this RabbitSettings settings)
    {
        return $"amqp://{settings.Username}:{settings.Password}@{settings.Uri}:{settings.Port}/{settings.VirtualHost}";
    }
}