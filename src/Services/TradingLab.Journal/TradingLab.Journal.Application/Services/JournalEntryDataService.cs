using FluentValidation;
using Microsoft.Extensions.Logging;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces;
using TradingLab.Journal.Domain.Exceptions;
using TradingLab.Journal.Application.Interfaces.Repositories;
using TradingLab.Journal.Application.Common.Pagination;

namespace TradingLab.Journal.Application.Services
{
	public class JournalEntryDataService: IJournalEntryDataService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<JournalEntryRequest> _validator;
        private readonly IValidator<JournalEntryFilterDto> _filterValidator;
        private readonly ILogger<JournalEntryDataService> _logger;

		public JournalEntryDataService(
            IUnitOfWork unitOfWork,
            IValidator<JournalEntryRequest> validator,
            IValidator<JournalEntryFilterDto> filterValidator,
            ILogger<JournalEntryDataService> logger)
		{
            _unitOfWork = unitOfWork;
            _validator = validator;
            _filterValidator = filterValidator;
            _logger = logger;
		}

        public async Task CreateAsync(JournalEntryRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(Guid.Empty);

            await _unitOfWork.JournalEntryRepository.CreateAsync(entity);

            _logger.LogInformation("Journal entry created");
        }

        public async Task DeleteAdync(Guid id)
        {
            var entity = await _unitOfWork.JournalEntryRepository.GetByIdAsync(id);

            await _unitOfWork.JournalEntryRepository.DeleteAsync(entity);

            _logger.LogInformation($"Journal entry with id={id} deleted");
        }

        public async Task<JournalEntryResponse> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.JournalEntryRepository.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("JournalEntry", id);

            var response = new JournalEntryResponse(entity);

            return response;
        }

        public async Task UpdateAsync(Guid id, JournalEntryRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(id);

            await _unitOfWork.JournalEntryRepository.UpdateAsync(entity);

            _logger.LogInformation($"Journal entry with id={id} updated");
        }

        public async Task<PaginatedList<JournalEntryResponse>> GetFilteredAsync(
            JournalEntryFilterDto filter, CancellationToken ct = default)
        {
            await _filterValidator.ValidateAndThrowAsync(filter, ct);

            var result = await _unitOfWork.JournalEntryRepository.GetFilteredAsync(filter, ct);

            return new PaginatedList<JournalEntryResponse>(
                    result.Items.Select(i => new JournalEntryResponse(i)).ToList(),
                    result.TotalCount,
                    result.PageNumber,
                    result.PageSize
                );
        }
    }
}

