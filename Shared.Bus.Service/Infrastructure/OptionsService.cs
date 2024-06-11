using Shared.Service.Domain;

namespace Shared.Bus.Service.Infrastructure
{
    public class OptionsService(string url, ushort port, string host, string user, string password, int prefetchCount, int concurrentMessageLimit) : IOptionsService
    {
        public string Url { get; init; } = url;
        public ushort Port { get; init; } = port;
        public string Host { get; init; } = host;
        public string User { get; init; } = user;
        public string Password { get; init; } = password;
        public int PrefetchCount { get; init; } = prefetchCount;
        public int ConcurrentMessageLimit { get; init; } = concurrentMessageLimit;
    }
}
