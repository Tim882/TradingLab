using System;
using System.Reflection.Metadata;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TradingLab.Journal.Application.DTOs
{
	public class TradeResponse : BaseResponse
    {
		public string Ticker { get; set; } = string.Empty;
		public Side Side { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public DateTime ExecutedAt { get; set; }

		public Guid TradingAccountId { get; set; }
		public Guid PositionId { get; set; }

        public IList<TradeNoteResponse> Notes { get; set; }

        public TradeResponse(Trade entity)
        {
            Id = entity.Id;
            Ticker = entity.Ticker;
            Side = entity.Side;
            Quantity = entity.Quantity;
            Price = entity.Price;
            ExecutedAt = entity.ExecutedAt;
            TradingAccountId = entity.TradingAccountId;
            PositionId = entity.PositionId;
            Notes = entity.Notes.Select(n => new TradeNoteResponse(n)).ToList();
        }
    }
}

