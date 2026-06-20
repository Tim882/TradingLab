using System;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Domain.Entities
{
	public class Position: BaseEntity
	{
		public string Ticker { get; set; } = string.Empty;
		public Direction Direction { get; set; }
		public Status Status { get; set; }
		public decimal TotalEntryQuantity { get; set; }
		public decimal AverageEntryPrice { get; set; }
		public decimal TotalExitQuantity { get; set; }
		public decimal AverageExitPrice { get; set; }
		public decimal RealizedPnL { get; set; }
		public DateTime OpenedAt { get; set; }
		public DateTime ClosedAt { get; set; }

		public Guid TradingAccountId { get; set; }
		public TradingAccount TradingAccount { get; set; } = null!;

		public List<Trade> Trades { get; set; } = new();
	}
}

