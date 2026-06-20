using System;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class PositionRequest
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

		public Position ToEntity(Guid id)
		{
			Position entity = new Position
			{
				Id = id,
				Ticker = Ticker,
				Direction = Direction,
				Status = Status,
				TotalEntryQuantity = TotalEntryQuantity,
				AverageEntryPrice = AverageEntryPrice,
				TotalExitQuantity = TotalExitQuantity,
				AverageExitPrice = AverageExitPrice,
				RealizedPnL = RealizedPnL,
				OpenedAt = OpenedAt,
				ClosedAt = ClosedAt,
				TradingAccountId = TradingAccountId
			};

			return entity;
		}
	}
}

