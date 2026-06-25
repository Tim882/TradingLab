using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces.Repositories;

namespace TradingLab.Journal.Application.Validators
{
	public class JournalEntryRequestValidator: AbstractValidator<JournalEntryRequest>
    {
		public JournalEntryRequestValidator(IUnitOfWork unitOfWork)
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

