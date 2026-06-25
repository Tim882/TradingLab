using System;
using FluentValidation;
using TradingLab.Journal.Application.Common.Pagination;

namespace TradingLab.Journal.Application.Validators
{
    public class PaginationValidator : AbstractValidator<PaginationParams>
    {
        public PaginationValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}