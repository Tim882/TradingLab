using System;
namespace TradingLab.Journal.Domain.Entities
{
	public class TradingAccount: BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public List<Position> Positions { get; set; } = new();
		public List<Trade> Trades { get; set; } = new();
	}
}

