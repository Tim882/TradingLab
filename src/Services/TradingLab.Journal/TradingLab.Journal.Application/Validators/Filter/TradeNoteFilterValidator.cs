using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class TradeNoteFilterValidator : AbstractValidator<TradeNoteFilterDto>
	{
        private static readonly string[] AllowedSortColumns =
			{ "Date" };

        private static string[] ValidTradeNoteTypes = {
            "PreTrade", "PostTrade", "General" };

        public TradeNoteFilterValidator()
		{
            Include(new PaginationValidator());

            When(x => !string.IsNullOrEmpty(x.SortBy), () =>
            {
                RuleFor(x => x.SortBy)
                    .Must(x => AllowedSortColumns.Contains(x))
                    .WithMessage($"Sort column must be one of: {string.Join(", ", AllowedSortColumns)}");
            });

            When(x => !string.IsNullOrEmpty(x.Type), () =>
            {
                RuleFor(x => x.Type)
                    .Must(x => ValidTradeNoteTypes.Contains(x))
                    .WithMessage($"Status must be one of: {string.Join(", ", ValidTradeNoteTypes)}");
            });

            RuleFor(x => x.FromDate)
            .LessThanOrEqualTo(x => x.ToDate)
            .When(x => x.FromDate.HasValue && x.ToDate.HasValue);
        }
	}
}

