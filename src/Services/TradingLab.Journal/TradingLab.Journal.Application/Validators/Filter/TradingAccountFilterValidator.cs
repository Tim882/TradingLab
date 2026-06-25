using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class TradingAccountFilterValidator : AbstractValidator<TradingAccountFilterDto>
	{
        public TradingAccountFilterValidator()
		{
            Include(new PaginationValidator());

            RuleFor(x => x.SearchTerm)
            .MaximumLength(100);
        }
	}
}

