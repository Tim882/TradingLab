using FluentValidation;
using Microsoft.Extensions.Logging;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Application.Services
{
	public class TradeDataService: ITradeDataService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TradeRequest> _validator;
        private readonly ILogger<TradeDataService> _logger;

		public TradeDataService(
            IUnitOfWork unitOfWork,
            IValidator<TradeRequest> validator,
            ILogger<TradeDataService> logger)
		{
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
		}

        public async Task CreateAsync(TradeRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(Guid.Empty);

            var tags = await GetTagsAsync(request.TagIds);

            entity.Tags = tags;

            await _unitOfWork.TradeRepository.CreateAsync(entity);

            _logger.LogInformation("Trade created");
        }

        public async Task DeleteAdync(Guid id)
        {
            var entity = await _unitOfWork.TradeRepository.GetByIdAsync(id);

            await _unitOfWork.TradeRepository.DeleteAsync(entity);

            _logger.LogInformation($"Trade with id={id} deleted");
        }

        public async Task<TradeResponse> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.TradeRepository.GetByIdAsync(id);
            var response = new TradeResponse(entity);

            return response;
        }

        public async Task UpdateAsync(Guid id, TradeRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(id);

            var tags = await GetTagsAsync(request.TagIds);

            entity.Tags = tags;

            await _unitOfWork.TradeRepository.UpdateAsync(entity);

            _logger.LogInformation($"Trade with id={id} updated");
        }

        protected async Task<List<Tag>> GetTagsAsync(List<Guid> tagIds)
        {
            var tags = new List<Tag>();

            foreach (var tagId in tagIds)
            {
                var tag = await _unitOfWork.TagRepository.GetByIdAsync(tagId);
                tags.Add(tag);
            }

            return tags;
        }
    }
}

