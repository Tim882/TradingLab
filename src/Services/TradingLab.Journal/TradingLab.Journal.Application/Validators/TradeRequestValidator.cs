using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Application.Validators
{
	public class TradeRequestValidator: AbstractValidator<TradeRequest>
    {
		private readonly IUnitOfWork _unitOfWork;

		public TradeRequestValidator(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

			RuleFor(e => e.PositionId)
				.NotEmpty()
				.MustAsync(PositionExistsAsync)
				.WithMessage("Position with ID '{PropertyValue}' does not exist");

			RuleFor(e => e.Ticker)
				.NotEmpty()
                .MaximumLength(100)
				.WithMessage("Trade must have a nonempty ticker with max legth less than 100");

			RuleFor(e => e.TradingAccountId)
                .NotEmpty()
				.MustAsync(TradingAccountExistsAsync)
                .WithMessage("Trading account with ID '{PropertyValue}' does not exist");
        }

		private async Task<bool> PositionExistsAsync(Guid id, CancellationToken ct)
		{
			return await _unitOfWork.PositionRepository.ExistsAsync(id);
		}

        private async Task<bool> TradingAccountExistsAsync(Guid id, CancellationToken ct)
        {
            return await _unitOfWork.TradingAccountRepository.ExistsAsync(id);
        }
    }
}

