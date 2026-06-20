using FluentValidation;
using Microsoft.Extensions.Logging;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces;
using TradingLab.Journal.Domain.Exceptions;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Application.Services
{
	public class TradingAccountDataService: ITradingAccountDataService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TradingAccountRequest> _validator;
        private readonly ILogger<TradingAccountDataService> _logger;

		public TradingAccountDataService(
            IUnitOfWork unitOfWork,
            IValidator<TradingAccountRequest> validator,
            ILogger<TradingAccountDataService> logger)
		{
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
		}

        public async Task CreateAsync(TradingAccountRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(Guid.Empty);

            await _unitOfWork.TradingAccountRepository.CreateAsync(entity);

            _logger.LogInformation("Trading account created");
        }

        public async Task DeleteAdync(Guid id)
        {
            var entity = await _unitOfWork.TradingAccountRepository.GetByIdAsync(id);

            await _unitOfWork.TradingAccountRepository.DeleteAsync(entity);

            _logger.LogInformation($"Trading account with id={id} deleted");
        }

        public async Task<TradingAccountResponse> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.TradingAccountRepository.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("TradingAccount", id);

            var response = new TradingAccountResponse(entity);

            return response;
        }

        public async Task UpdateAsync(Guid id, TradingAccountRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(id);

            await _unitOfWork.TradingAccountRepository.UpdateAsync(entity);

            _logger.LogInformation($"Trading account with id={id} updated");
        }
    }
}

