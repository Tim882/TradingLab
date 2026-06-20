using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Domain.Interfaces.Repositories
{
	public interface ITradeRepository
	{
		public Task<Trade> GetByIdAsync(Guid id);
		public Task CreateAsync(Trade entity);
		public Task UpdateAsync(Trade entity);
		public Task DeleteAsync(Trade entity);
        public Task<bool> ExistsAsync(Guid id);
    }
}

