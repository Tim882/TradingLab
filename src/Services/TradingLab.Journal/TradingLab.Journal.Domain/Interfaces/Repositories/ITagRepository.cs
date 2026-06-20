using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Domain.Interfaces.Repositories
{
	public interface ITagRepository
	{
		public Task<Tag> GetByIdAsync(Guid id);
		public Task CreateAsync(Tag entity);
		public Task UpdateAsync(Tag entity);
		public Task DeleteAsync(Tag entity);
        public Task<bool> ExistsAsync(Guid id);
    }
}

