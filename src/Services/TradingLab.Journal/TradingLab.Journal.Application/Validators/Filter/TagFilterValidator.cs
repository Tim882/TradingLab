using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class TagFilterValidator : AbstractValidator<TagFilterDto>
	{
        public TagFilterValidator()
		{
            Include(new PaginationValidator());

            RuleFor(x => x.SearchTerm)
            .MaximumLength(100);
        }
	}
}

