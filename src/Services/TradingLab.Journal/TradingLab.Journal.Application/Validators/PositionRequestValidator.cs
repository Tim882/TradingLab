using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Application.Validators
{
	public class PositionRequestValidator: AbstractValidator<PositionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionRequestValidator(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;

			RuleFor(e => e.Ticker)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Position must have a nonempty ticker with max legth less than 100");

            RuleFor(e => e.TradingAccountId)
                .NotEmpty()
                .MustAsync(TradingAccountExistsAsync)
                .WithMessage("Trading account with ID '{PropertyValue}' does not exist");
        }

        private async Task<bool> TradingAccountExistsAsync(Guid id, CancellationToken ct)
        {
            return await _unitOfWork.TradingAccountRepository.ExistsAsync(id);
        }
	}
}

