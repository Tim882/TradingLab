using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class PositionFilterDto: PaginationParams
	{
        public string? Direction { get; set; }
        public string? Status { get; set; }
        public decimal? MinTotalEntryQuantity { get; set; }
        public decimal? MaxTotalEntryQuantity { get; set; }
        public decimal? MinAverageEntryPrice { get; set; }
        public decimal? MaxAverageEntryPrice { get; set; }
        public decimal? MinTotalExitQuantity { get; set; }
        public decimal? MaxTotalExitQuantity { get; set; }
        public decimal? MinAverageExitPrice { get; set; }
        public decimal? MaxAverageExitPrice { get; set; }
        public decimal? MinRealizedPnL { get; set; }
        public decimal? MaxRealizedPnL { get; set; }
        public DateTime? FromOpenedAt { get; set; }
        public DateTime? ToOpenedAt { get; set; }
        public DateTime? FromClosedAt { get; set; }
        public DateTime? ToClosedAt { get; set; }
        public string? SearchTerm { get; set; }
    }
}

