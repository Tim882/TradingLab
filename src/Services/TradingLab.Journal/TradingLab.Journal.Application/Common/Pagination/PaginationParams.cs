using System;
namespace TradingLab.Journal.Application.Common.Pagination
{
    public class PaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = true;
    }
}

