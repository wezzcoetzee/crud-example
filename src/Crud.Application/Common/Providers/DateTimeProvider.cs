using Crud.Application.Common.Interfaces;

namespace Crud.Application.Common.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}