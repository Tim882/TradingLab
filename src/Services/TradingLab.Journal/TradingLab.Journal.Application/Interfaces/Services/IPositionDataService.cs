using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Interfaces
{
	public interface IPositionDataService
	{
		public Task<PositionResponse> GetByIdAsync(Guid id);
		public Task CreateAsync(PositionRequest request);
		public Task UpdateAsync(Guid id, PositionRequest request);
		public Task DeleteAdync(Guid id);
        public Task<PaginatedList<PositionResponse>> GetFilteredAsync(
            PositionFilterDto filter,
            CancellationToken ct = default);
    }
}

