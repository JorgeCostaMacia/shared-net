using Serilog.Core;
using Serilog.Events;

namespace JorgeCostaMacia.Quartz.Serilog.Tests.Fakes;

/// <summary>Serilog sink double — captures every emitted event for assertion (the in-memory sink package misses events across execution contexts; a plain capture does not).</summary>
internal sealed class CapturingSink : ILogEventSink
{
    /// <summary>The captured events, in emission order.</summary>
    public List<LogEvent> Events { get; } = [];

    public void Emit(LogEvent logEvent) => Events.Add(logEvent);
}
