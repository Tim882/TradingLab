using System;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Domain.Entities
{
	public class Trade: BaseEntity
	{
		public string Ticker { get; set; } = string.Empty;
		public Side Side { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public DateTime ExecutedAt { get; set; }

		public Guid TradingAccountId { get; set; }
		public Guid PositionId { get; set; }

		public TradingAccount TradingAccount { get; set; } = null!;
		public Position Position { get; set; } = null!;

		public List<TradeNote> Notes { get; set; } = new();
		public List<Tag> Tags { get; set; } = new();
	}
}

