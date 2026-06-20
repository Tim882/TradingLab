using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Domain.Interfaces.Repositories
{
	public interface IPositionRepository
	{
		public Task<Position> GetByIdAsync(Guid id);
		public Task CreateAsync(Position entity);
		public Task UpdateAsync(Position entity);
		public Task DeleteAsync(Position entity);
	}
}

