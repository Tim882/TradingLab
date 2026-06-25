using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class TradeNoteFilterDto : PaginationParams
    {
        public string? Type { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? SearchTerm { get; set; }
    }
}

