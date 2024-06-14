using Shared.Service.Domain;

namespace Shared.Bus.Service.Infrastructure;

public class OptionsService : IOptionsService
{
    public string Url { get; init; }
    public ushort Port { get; init; }
    public string Host { get; init; }
    public string User { get; init; }
    public string Password { get; init; }
    public int PrefetchCount { get; init; }
    public int ConcurrentMessageLimit { get; init; }

    public OptionsService(string url, ushort port, string host, string user, string password, int prefetchCount, int concurrentMessageLimit)
    {
        Url = url;
        Port = port;
        Host = host;
        User = user;
        Password = password;
        PrefetchCount = prefetchCount;
        ConcurrentMessageLimit = concurrentMessageLimit;
    }
}
