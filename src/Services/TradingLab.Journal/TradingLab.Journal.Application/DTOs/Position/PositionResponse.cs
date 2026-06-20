using System;
using System.Reflection.Metadata;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TradingLab.Journal.Application.DTOs
{
	public class PositionResponse : BaseResponse
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

        public PositionResponse(Position entity)
        {
            Id = entity.Id;
            Ticker = entity.Ticker;
            Direction = entity.Direction;
            Status = entity.Status;
            TotalEntryQuantity = entity.TotalEntryQuantity;
            AverageEntryPrice = entity.AverageEntryPrice;
            TotalExitQuantity = entity.TotalExitQuantity;
            AverageExitPrice = entity.AverageExitPrice;
            RealizedPnL = entity.RealizedPnL;
            OpenedAt = entity.OpenedAt;
            ClosedAt = entity.ClosedAt;
            TradingAccountId = entity.TradingAccountId;
        }
    }
}

