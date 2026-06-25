using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Interfaces
{
	public interface ITradeDataService
	{
		public Task<TradeResponse> GetByIdAsync(Guid id);
		public Task CreateAsync(TradeRequest request);
		public Task UpdateAsync(Guid id, TradeRequest request);
		public Task DeleteAdync(Guid id);
        public Task<PaginatedList<TradeResponse>> GetFilteredAsync(
            TradeFilterDto filter,
            CancellationToken ct = default);
    }
}

