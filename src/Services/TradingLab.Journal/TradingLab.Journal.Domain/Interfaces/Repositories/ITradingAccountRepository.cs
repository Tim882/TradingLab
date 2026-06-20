using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Domain.Interfaces.Repositories
{
	public interface ITradingAccountRepository
	{
		public Task<TradingAccount> GetByIdAsync(Guid id);
		public Task CreateAsync(TradingAccount entity);
		public Task UpdateAsync(TradingAccount entity);
		public Task DeleteAsync(TradingAccount entity);
	}
}

