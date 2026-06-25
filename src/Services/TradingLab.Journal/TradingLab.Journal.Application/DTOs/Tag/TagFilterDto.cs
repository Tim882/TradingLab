using System;
using TradingLab.Journal.Application.Common.Pagination;

namespace TradingLab.Journal.Application.DTOs
{
	public class TagFilterDto : PaginationParams
    {
		public string? SearchTerm { get; set; }
	}
}

