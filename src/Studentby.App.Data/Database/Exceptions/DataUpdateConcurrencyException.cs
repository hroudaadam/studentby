using Studentby.Shared.Exceptions;

namespace Studentby.App.Data.Database.Exceptions;

public class DataUpdateConcurrencyException : BadLogicException
{
    public DataUpdateConcurrencyException() : base("DataUpdateConcurrency")
    {
    }
}
