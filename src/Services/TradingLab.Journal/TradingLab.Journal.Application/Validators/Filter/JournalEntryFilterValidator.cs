using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class JournalEntryFilterValidator : AbstractValidator<JournalEntryFilterDto>
	{
		private static readonly string[] AllowedSortColumns =
			{ "Date", "Mood" };

        private static readonly string[] ValidMarketConditions =
            { "Bullish", "Bearish", "Sideways", "Choppy" };

        public JournalEntryFilterValidator()
		{
            Include(new PaginationValidator());

            When(x => !string.IsNullOrEmpty(x.SortBy), () =>
            {
                RuleFor(x => x.SortBy)
                    .Must(x => AllowedSortColumns.Contains(x))
                    .WithMessage($"Sort column must be one of: {string.Join(", ", AllowedSortColumns)}");
            });

            When(x => !string.IsNullOrEmpty(x.MarketCondition), () =>
            {
                RuleFor(x => x.MarketCondition)
                    .Must(x => ValidMarketConditions.Contains(x))
                    .WithMessage($"MarketCondition must be one of: {string.Join(", ", ValidMarketConditions)}");
            });

            RuleFor(x => x.MinMood)
            .LessThanOrEqualTo(x => x.MaxMood)
            .When(x => x.MinMood.HasValue && x.MaxMood.HasValue);

            RuleFor(x => x.FromDate)
                .LessThanOrEqualTo(x => x.ToDate)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue);

            RuleFor(x => x.SearchTerm)
                .MaximumLength(100);
        }
	}
}

