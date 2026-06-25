using System;
using TradingLab.Journal.Application.Common.Pagination;

namespace TradingLab.Journal.Application.DTOs
{
	public class TradeFilterDto : PaginationParams
    {
        public string? Pair { get; set; }
        public string? Side { get; set; }
        public decimal? MinQuantity { get; set; }
        public decimal? MaxQuantity { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Status { get; set; }
        public Guid? TradingAccountId { get; set; }
        public List<Guid>? TagIds { get; set; }
        public string? SearchTerm { get; set; }
    }
}

