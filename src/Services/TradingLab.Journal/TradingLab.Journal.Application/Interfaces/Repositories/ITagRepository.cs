using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.Interfaces.Repositories
{
	public interface ITagRepository
	{
        public Task<PaginatedList<Tag>> GetFilteredAsync(
            TagFilterDto filter, CancellationToken ct);
        public Task<Tag> GetByIdAsync(Guid id);
		public Task CreateAsync(Tag entity);
		public Task UpdateAsync(Tag entity);
		public Task DeleteAsync(Tag entity);
        public Task<bool> ExistsAsync(Guid id);
    }
}

