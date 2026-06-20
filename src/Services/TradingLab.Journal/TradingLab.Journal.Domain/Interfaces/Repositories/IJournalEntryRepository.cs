using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Domain.Interfaces.Repositories
{
	public interface IJournalEntryRepository
	{
		public Task<JournalEntry> GetByIdAsync(Guid id);
		public Task CreateAsync(JournalEntry entity);
		public Task UpdateAsync(JournalEntry entity);
		public Task DeleteAsync(JournalEntry entity);
	}
}

