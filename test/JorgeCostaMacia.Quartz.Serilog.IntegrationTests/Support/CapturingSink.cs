using Serilog.Core;
using Serilog.Events;

namespace JorgeCostaMacia.Quartz.Serilog.IntegrationTests.Support;

/// <summary>
/// Thread-safe Serilog sink double — captures every emitted event for assertion. Unlike the unit-test
/// capture, a real scheduler emits from Quartz worker threads while the test polls, so both the write
/// and the snapshot are guarded.
/// </summary>
internal sealed class CapturingSink : ILogEventSink
{
    private readonly object gate = new();
    private readonly List<LogEvent> events = [];

    public void Emit(LogEvent logEvent)
    {
        lock (gate)
        {
            events.Add(logEvent);
        }
    }

    /// <summary>A stable copy of the events captured so far, in emission order.</summary>
    public IReadOnlyList<LogEvent> Snapshot()
    {
        lock (gate)
        {
            return [.. events];
        }
    }
}
