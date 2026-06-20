using System;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Domain.Entities
{
	public class JournalEntry: BaseEntity
	{
		public DateOnly Date { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public int Mood { get; set; }
		public MarketCondition MarketCondition { get; set; }
	}
}

