using System;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class JournalEntryRequest
	{
		public DateOnly Date { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public int Mood { get; set; }
		public MarketCondition MarketCondition { get; set; }

		public JournalEntry ToEntity(Guid id)
		{
			JournalEntry entity = new JournalEntry
			{
				Id = id,
				Date = Date,
				Title = Title,
				Content = Content,
				Mood = Mood,
				MarketCondition = MarketCondition
			};

			return entity;
		}
	}
}

