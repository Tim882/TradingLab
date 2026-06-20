using System;
using System.Diagnostics;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class TradeNoteResponse : BaseResponse
    {
		public TradeNoteType Type { get; set; }
		public string Content { get; set; } = string.Empty;
		public DateOnly Date { get; set; }

		public Guid TradeId { get; set; }

        public TradeNoteResponse(TradeNote entity)
        {
            Id = entity.Id;
            Type = entity.Type;
            Content = entity.Content;
            Date = entity.Date;
            TradeId = entity.TradeId;
        }
    }
}

