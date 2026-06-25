using FluentValidation;
using Microsoft.Extensions.Logging;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces;
using TradingLab.Journal.Domain.Exceptions;
using TradingLab.Journal.Application.Interfaces.Repositories;
using TradingLab.Journal.Application.Common.Pagination;

namespace TradingLab.Journal.Application.Services
{
	public class TagDataService: ITagDataService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TagRequest> _validator;
        private readonly IValidator<TagFilterDto> _filterValidator;
        private readonly ILogger<TagDataService> _logger;

		public TagDataService(
            IUnitOfWork unitOfWork,
            IValidator<TagRequest> validator,
            IValidator<TagFilterDto> filterValidator,
            ILogger<TagDataService> logger)
		{
            _unitOfWork = unitOfWork;
            _validator = validator;
            _filterValidator = filterValidator;
            _logger = logger;
		}

        public async Task CreateAsync(TagRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(Guid.Empty);

            await _unitOfWork.TagRepository.CreateAsync(entity);

            _logger.LogInformation("Tag created");
        }

        public async Task DeleteAdync(Guid id)
        {
            var entity = await _unitOfWork.TagRepository.GetByIdAsync(id);

            await _unitOfWork.TagRepository.DeleteAsync(entity);

            _logger.LogInformation($"Tag with id={id} deleted");
        }

        public async Task<TagResponse> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.TagRepository.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("Tag", id);

            var response = new TagResponse(entity);

            return response;
        }

        public async Task UpdateAsync(Guid id, TagRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var entity = request.ToEntity(id);

            await _unitOfWork.TagRepository.UpdateAsync(entity);

            _logger.LogInformation($"Tag with id={id} updated");
        }

        public async Task<PaginatedList<TagResponse>> GetFilteredAsync(
            TagFilterDto filter, CancellationToken ct = default)
        {
            await _filterValidator.ValidateAndThrowAsync(filter, ct);

            var result = await _unitOfWork.TagRepository.GetFilteredAsync(filter, ct);

            return new PaginatedList<TagResponse>(
                    result.Items.Select(i => new TagResponse(i)).ToList(),
                    result.TotalCount,
                    result.PageNumber,
                    result.PageSize
                );
        }
    }
}

