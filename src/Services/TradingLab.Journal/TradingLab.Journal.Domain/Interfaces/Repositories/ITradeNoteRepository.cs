using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Domain.Interfaces.Repositories
{
	public interface ITradeNoteRepository
	{
		public Task<TradeNote> GetByIdAsync(Guid id);
		public Task CreateAsync(TradeNote entity);
		public Task UpdateAsync(TradeNote entity);
		public Task DeleteAsync(TradeNote entity);
	}
}

