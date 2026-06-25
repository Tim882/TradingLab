using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.Interfaces.Repositories
{
	public interface ITradingAccountRepository
	{
        public Task<PaginatedList<TradingAccount>> GetFilteredAsync(
            TradingAccountFilterDto filter, CancellationToken ct);
        public Task<TradingAccount> GetByIdAsync(Guid id);
		public Task CreateAsync(TradingAccount entity);
		public Task UpdateAsync(TradingAccount entity);
		public Task DeleteAsync(TradingAccount entity);
        public Task<bool> ExistsAsync(Guid id);
    }
}

