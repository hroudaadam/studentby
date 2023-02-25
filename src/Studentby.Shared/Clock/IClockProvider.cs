namespace Studentby.Shared.Clock;

public interface IClockProvider
{
    DateTime Now { get; }
}