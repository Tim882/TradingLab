using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.DTOs
{
	public class TradingAccountResponse : BaseResponse
    {
		public string Name { get; set; } = string.Empty;

        public TradingAccountResponse(TradingAccount entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}

