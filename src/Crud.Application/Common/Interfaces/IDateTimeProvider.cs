namespace Crud.Application.Common.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow => DateTime.UtcNow;
}