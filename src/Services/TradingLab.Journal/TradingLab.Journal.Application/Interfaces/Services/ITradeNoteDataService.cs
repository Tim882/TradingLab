using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Interfaces
{
	public interface ITradeNoteDataService
	{
		public Task<TradeNoteResponse> GetByIdAsync(Guid id);
		public Task CreateAsync(TradeNoteRequest request);
		public Task UpdateAsync(Guid id, TradeNoteRequest request);
		public Task DeleteAdync(Guid id);
        public Task<PaginatedList<TradeNoteResponse>> GetFilteredAsync(
            TradeNoteFilterDto filter,
            CancellationToken ct = default);
    }
}

