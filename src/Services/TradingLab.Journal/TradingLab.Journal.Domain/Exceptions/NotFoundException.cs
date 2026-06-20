using System;
namespace TradingLab.Journal.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string resourceName, object key)
            : base($"{resourceName} with key '{key}' was not found", "NOT_FOUND")
        {
        }
    }
}

