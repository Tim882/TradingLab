using System;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class TradeNoteRequest
    {
		public TradeNoteType Type { get; set; }
		public string Content { get; set; } = string.Empty;
		public DateOnly Date { get; set; }

		public Guid TradeId { get; set; }

		public TradeNote ToEntity(Guid id)
		{
			TradeNote entity = new TradeNote
			{
				Id = id,
				Type = Type,
				Content = Content,
				Date = Date,
				TradeId = TradeId
			};

			return entity;
		}
	}
}

