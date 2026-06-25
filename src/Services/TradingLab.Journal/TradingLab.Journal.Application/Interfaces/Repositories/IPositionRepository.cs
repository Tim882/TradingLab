using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.Interfaces.Repositories
{
	public interface IPositionRepository
	{
		public Task<PaginatedList<Position>> GetFilteredAsync(
			PositionFilterDto filter, CancellationToken ct);
		public Task<Position> GetByIdAsync(Guid id);
		public Task CreateAsync(Position entity);
		public Task UpdateAsync(Position entity);
		public Task DeleteAsync(Position entity);
        public Task<bool> ExistsAsync(Guid id);
    }
}

