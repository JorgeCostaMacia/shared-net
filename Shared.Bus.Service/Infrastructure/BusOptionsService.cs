using Shared.Service.Domain;

namespace Shared.Bus.Service.Infrastructure
{
    public class BusOptionsService : IOptionsService
    {
        public string Url { get; set; } = string.Empty;
        public ushort Port { get; set; } = 0;
        public string Host { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int PrefetchCount { get; set; } = 4;
        public int ConcurrentMessageLimit { get; set; } = 2;
    }
}
