using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class TradeRequestValidator: AbstractValidator<TradeRequest>
    {
		public TradeRequestValidator()
		{
			RuleFor(e => e.PositionId)
				.NotEmpty()
				.WithMessage("Position with ID '{PropertyValue}' does not exist");

			RuleFor(e => e.Ticker)
				.NotEmpty()
                .MaximumLength(100)
				.WithMessage("Trade must have a nonempty ticker with max legth less than 100");

			RuleFor(e => e.TradingAccountId)
                .NotEmpty()
                .WithMessage("Trading account with ID '{PropertyValue}' does not exist");
        }
	}
}

