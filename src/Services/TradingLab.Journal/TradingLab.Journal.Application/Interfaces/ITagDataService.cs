using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Interfaces
{
	public interface ITagDataService
	{
		public Task<TagResponse> GetByIdAsync(Guid id);
		public Task CreateAsync(TagRequest request);
		public Task UpdateAsync(Guid id, TagRequest request);
		public Task DeleteAdync(Guid id);
	}
}

