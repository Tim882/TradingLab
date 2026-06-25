using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.Interfaces.Repositories
{
	public interface ITradeRepository
	{
        public Task<PaginatedList<Trade>> GetFilteredAsync(
            TradeFilterDto filter, CancellationToken ct);
        public Task<Trade> GetByIdAsync(Guid id);
		public Task CreateAsync(Trade entity);
		public Task UpdateAsync(Trade entity);
		public Task DeleteAsync(Trade entity);
        public Task<bool> ExistsAsync(Guid id);
    }
}

