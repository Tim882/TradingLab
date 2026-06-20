using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Application.Validators
{
	public class TradeNoteRequestValidator: AbstractValidator<TradeNoteRequest>
    {
		private readonly IUnitOfWork _unitOfWork;

		public TradeNoteRequestValidator(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

			RuleFor(e => e.Content)
				.NotEmpty()
				.WithMessage("TradeNote must have a text");

			RuleFor(e => e.TradeId)
				.NotEmpty()
				.MustAsync(TradeExistsAsync)
				.WithMessage("Trade with ID '{PropertyValue}' does not exist");
		}

		private async Task<bool> TradeExistsAsync(Guid id, CancellationToken ct)
		{
			return await _unitOfWork.TradeRepository.ExistsAsync(id);
		}
	}
}

