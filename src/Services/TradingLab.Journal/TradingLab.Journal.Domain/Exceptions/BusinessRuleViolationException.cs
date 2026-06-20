using System;
namespace TradingLab.Journal.Domain.Exceptions
{
    public class BusinessRuleViolationException : DomainException
    {
        public BusinessRuleViolationException(string message)
            : base(message, "BUSINESS_RULE_VIOLATION")
        {
        }
    }
}

