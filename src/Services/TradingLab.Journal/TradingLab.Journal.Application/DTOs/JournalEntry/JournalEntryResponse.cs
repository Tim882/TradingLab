using System;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class JournalEntryResponse: BaseResponse
    {
		public DateOnly Date { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public int Mood { get; set; }
		public MarketCondition MarketCondition { get; set; }

		public JournalEntryResponse(JournalEntry entity)
		{
			Id = entity.Id;
			Date = entity.Date;
            Title = entity.Title;
            Content = entity.Content;
            Mood = entity.Mood;
            MarketCondition = entity.MarketCondition;
        }
	}
}

