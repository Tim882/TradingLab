using System;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class JournalEntryFilterDto: PaginationParams
    {
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public int? MinMood { get; set; }
        public int? MaxMood { get; set; }
        public string? MarketCondition { get; set; }
        public string? SearchTerm { get; set; }
    }
}

