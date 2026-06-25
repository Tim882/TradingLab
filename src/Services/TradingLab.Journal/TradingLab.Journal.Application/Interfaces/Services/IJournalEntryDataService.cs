using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Interfaces
{
	public interface IJournalEntryDataService
	{
		public Task<JournalEntryResponse> GetByIdAsync(Guid id);
		public Task CreateAsync(JournalEntryRequest request);
		public Task UpdateAsync(Guid id, JournalEntryRequest request);
		public Task DeleteAdync(Guid id);
		public Task<PaginatedList<JournalEntryResponse>> GetFilteredAsync(
			JournalEntryFilterDto filter,
			CancellationToken ct = default);

    }
}

