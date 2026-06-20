using System;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Domain.Entities
{
	public class TradeNote: BaseEntity
	{
		public TradeNoteType Type { get; set; }
		public string Content { get; set; } = string.Empty;
		public DateOnly Date { get; set; }

		public Guid TradeId { get; set; }
		public Trade Trade { get; set; } = null!;
	}
}

