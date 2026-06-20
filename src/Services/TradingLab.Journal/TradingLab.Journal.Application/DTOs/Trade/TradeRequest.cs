using System;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class TradeRequest
    {
		public string Ticker { get; set; } = string.Empty;
		public Side Side { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public DateTime ExecutedAt { get; set; }

		public Guid TradingAccountId { get; set; }
		public Guid PositionId { get; set; }

		public List<Guid> TagIds { get; set; } = new();

		public Trade ToEntity(Guid id)
		{
			Trade entity = new Trade
			{
				Id = id,
				Ticker = Ticker,
				Side = Side,
				Quantity = Quantity,
				Price = Price,
				ExecutedAt = ExecutedAt,
				TradingAccountId = TradingAccountId,
				PositionId = PositionId
			};

			return entity;
		}
	}
}

