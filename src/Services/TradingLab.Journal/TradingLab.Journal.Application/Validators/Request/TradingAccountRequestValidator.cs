using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class TradingAccountRequestValidator: AbstractValidator<TradingAccountRequest>
    {
		public TradingAccountRequestValidator()
		{
			RuleFor(e => e.Name)
				.NotEmpty()
				.WithMessage("Trading account must have a name");
		}
	}
}

