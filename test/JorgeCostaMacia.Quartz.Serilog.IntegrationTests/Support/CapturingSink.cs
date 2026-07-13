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
    private readonly object _gate = new object();
    private readonly List<LogEvent> _events = new List<LogEvent>();

    public void Emit(LogEvent logEvent)
    {
        lock (_gate)
        {
            _events.Add(logEvent);
        }
    }

    /// <summary>A stable copy of the events captured so far, in emission order.</summary>
    public IReadOnlyList<LogEvent> Snapshot()
    {
        lock (_gate)
        {
            return _events.ToList();
        }
    }
}
