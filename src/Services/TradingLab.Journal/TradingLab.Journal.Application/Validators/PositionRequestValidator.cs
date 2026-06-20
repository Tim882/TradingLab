using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class PositionRequestValidator: AbstractValidator<PositionRequest>
    {
		public PositionRequestValidator()
		{
			RuleFor(e => e.Ticker)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Position must have a nonempty ticker with max legth less than 100");

            RuleFor(e => e.TradingAccountId)
                .NotEmpty()
                .WithMessage("Trading account with ID '{PropertyValue}' does not exist");
        }
	}
}

