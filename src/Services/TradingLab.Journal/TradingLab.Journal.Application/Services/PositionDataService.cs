using FluentValidation;
using Microsoft.Extensions.Logging;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces;
using TradingLab.Journal.Domain.Exceptions;
using TradingLab.Journal.Application.Interfaces.Repositories;
using TradingLab.Journal.Application.Common.Pagination;

namespace TradingLab.Journal.Application.Services
{
	public class PositionDataService: IPositionDataService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PositionRequest> _validator;
        private readonly IValidator<PositionFilterDto> _filterValidator;
        private readonly ILogger<PositionDataService> _logger;

		public PositionDataService(
            IUnitOfWork unitOfWork,
            IValidator<PositionRequest> validator,
            IValidator<PositionFilterDto> filterValidator,
            ILogger<PositionDataService> logger)
		{
            _unitOfWork = unitOfWork;
            _validator = validator;
            _filterValidator = filterValidator;
            _logger = logger;
		}

        public async Task CreateAsync(PositionRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(Guid.Empty);

            await _unitOfWork.PositionRepository.CreateAsync(entity);

            _logger.LogInformation("Position created");
        }

        public async Task DeleteAdync(Guid id)
        {
            var entity = await _unitOfWork.PositionRepository.GetByIdAsync(id);

            await _unitOfWork.PositionRepository.DeleteAsync(entity);

            _logger.LogInformation($"Position with id={id} deleted");
        }

        public async Task<PositionResponse> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.PositionRepository.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("Position", id);

            var response = new PositionResponse(entity);

            return response;
        }

        public async Task UpdateAsync(Guid id, PositionRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(id);

            await _unitOfWork.PositionRepository.UpdateAsync(entity);

            _logger.LogInformation($"Position with id={id} updated");
        }

        public async Task<PaginatedList<PositionResponse>> GetFilteredAsync(
            PositionFilterDto filter, CancellationToken ct = default)
        {
            await _filterValidator.ValidateAndThrowAsync(filter, ct);

            var result = await _unitOfWork.PositionRepository.GetFilteredAsync(filter, ct);

            return new PaginatedList<PositionResponse>(
                    result.Items.Select(i => new PositionResponse(i)).ToList(),
                    result.TotalCount,
                    result.PageNumber,
                    result.PageSize
                );
        }
    }
}

