using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Interfaces
{
	public interface ITradingAccountDataService
	{
		public Task<TradingAccountResponse> GetByIdAsync(Guid id);
		public Task CreateAsync(TradingAccountRequest request);
		public Task UpdateAsync(Guid id, TradingAccountRequest request);
		public Task DeleteAdync(Guid id);
	}
}

