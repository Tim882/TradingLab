using System;
namespace TradingLab.Journal.Domain.Entities
{
	public class Tag: BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public List<Trade> Trades { get; set; } = new();
	}
}

