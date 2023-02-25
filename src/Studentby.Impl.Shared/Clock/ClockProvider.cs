using Studentby.Shared.Clock;

namespace Studentby.Impl.Shared.Clock;

public class ClockProvider : IClockProvider
{
    public DateTime Now => DateTime.UtcNow;
}
