using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.Interfaces.Repositories
{
	public interface ITradeNoteRepository
	{
        public Task<PaginatedList<TradeNote>> GetFilteredAsync(
            TradeNoteFilterDto filter, CancellationToken ct);
        public Task<TradeNote> GetByIdAsync(Guid id);
		public Task CreateAsync(TradeNote entity);
		public Task UpdateAsync(TradeNote entity);
		public Task DeleteAsync(TradeNote entity);
        public Task<bool> ExistsAsync(Guid id);
    }
}

