using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class JournalEntryRequestValidator: AbstractValidator<JournalEntryRequest>
    {
		public JournalEntryRequestValidator()
		{
			RuleFor(e => e.Mood)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(10)
				.WithMessage("Mood must be in range between 1 and 10");

			RuleFor(e => e.Title)
				.NotEmpty()
				.WithMessage("Title must not be empty");
		}
	}
}

