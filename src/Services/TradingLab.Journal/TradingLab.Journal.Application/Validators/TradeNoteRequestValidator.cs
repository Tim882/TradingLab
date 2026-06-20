using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class TradeNoteRequestValidator: AbstractValidator<TradeNoteRequest>
    {
		public TradeNoteRequestValidator()
		{
			RuleFor(e => e.Content)
				.NotEmpty()
				.WithMessage("TradeNote must have a text");

			RuleFor(e => e.TradeId)
				.NotEmpty()
				.WithMessage("Trade with ID '{PropertyValue}' does not exist");
		}
	}
}

