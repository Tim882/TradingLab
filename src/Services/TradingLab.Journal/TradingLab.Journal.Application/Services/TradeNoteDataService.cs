using FluentValidation;
using Microsoft.Extensions.Logging;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Application.Services
{
	public class TradeNoteDataService: ITradeNoteDataService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TradeNoteRequest> _validator;
        private readonly ILogger<TradeNoteDataService> _logger;

		public TradeNoteDataService(
            IUnitOfWork unitOfWork,
            IValidator<TradeNoteRequest> validator,
            ILogger<TradeNoteDataService> logger)
		{
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
		}

        public async Task CreateAsync(TradeNoteRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(Guid.Empty);

            await _unitOfWork.TradeNoteRepository.CreateAsync(entity);

            _logger.LogInformation("Trade note created");
        }

        public async Task DeleteAdync(Guid id)
        {
            var entity = await _unitOfWork.TradeNoteRepository.GetByIdAsync(id);

            await _unitOfWork.TradeNoteRepository.DeleteAsync(entity);

            _logger.LogInformation($"Trade note with id={id} deleted");
        }

        public async Task<TradeNoteResponse> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.TradeNoteRepository.GetByIdAsync(id);
            var response = new TradeNoteResponse(entity);

            return response;
        }

        public async Task UpdateAsync(Guid id, TradeNoteRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(id);

            await _unitOfWork.TradeNoteRepository.UpdateAsync(entity);

            _logger.LogInformation($"Trade note with id={id} updated");
        }
    }
}

