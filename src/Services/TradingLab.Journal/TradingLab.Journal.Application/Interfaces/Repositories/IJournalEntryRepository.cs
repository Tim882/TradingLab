using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.Interfaces.Repositories
{
	public interface IJournalEntryRepository
	{
		public Task<PaginatedList<JournalEntry>> GetFilteredAsync(
			JournalEntryFilterDto filter, CancellationToken ct);
		public Task<JournalEntry> GetByIdAsync(Guid id);
		public Task CreateAsync(JournalEntry entity);
		public Task UpdateAsync(JournalEntry entity);
		public Task DeleteAsync(JournalEntry entity);
		public Task<bool> ExistsAsync(Guid id);
	}
}

