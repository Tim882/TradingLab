using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class PositionFilterValidator : AbstractValidator<PositionFilterDto>
	{
        private static readonly string[] AllowedSortColumns =
			{ "TotalEntryQuantity", "AverageEntryPrice", "TotalExitQuantity",
            "AverageExitPrice", "RealizedPnL" };

        private static readonly string[] ValidStatuses = { "Open", "Close" };

        private static readonly string[] ValidDirections = { "Long", "Short" };

        public PositionFilterValidator()
		{
            Include(new PaginationValidator());

            When(x => !string.IsNullOrEmpty(x.SortBy), () =>
            {
                RuleFor(x => x.SortBy)
                    .Must(x => AllowedSortColumns.Contains(x))
                    .WithMessage($"Sort column must be one of: {string.Join(", ", AllowedSortColumns)}");
            });

            When(x => !string.IsNullOrEmpty(x.Direction), () =>
            {
                RuleFor(x => x.Direction)
                    .Must(x => ValidDirections.Contains(x))
                    .WithMessage($"Direction must be one of: {string.Join(", ", ValidDirections)}");
            });

            When(x => !string.IsNullOrEmpty(x.Status), () =>
            {
                RuleFor(x => x.Status)
                    .Must(x => ValidStatuses.Contains(x))
                    .WithMessage($"Status must be one of: {string.Join(", ", ValidStatuses)}");
            });

            RuleFor(x => x.MinTotalEntryQuantity)
            .LessThanOrEqualTo(x => x.MaxTotalEntryQuantity)
            .When(x => x.MinTotalEntryQuantity.HasValue && x.MaxTotalEntryQuantity.HasValue);

            RuleFor(x => x.MinAverageEntryPrice)
            .LessThanOrEqualTo(x => x.MaxAverageEntryPrice)
            .When(x => x.MinAverageEntryPrice.HasValue && x.MaxAverageEntryPrice.HasValue);

            RuleFor(x => x.MinTotalExitQuantity)
            .LessThanOrEqualTo(x => x.MaxTotalExitQuantity)
            .When(x => x.MinTotalExitQuantity.HasValue && x.MaxTotalExitQuantity.HasValue);

            RuleFor(x => x.MinAverageExitPrice)
            .LessThanOrEqualTo(x => x.MaxAverageExitPrice)
            .When(x => x.MinAverageExitPrice.HasValue && x.MaxAverageExitPrice.HasValue);

            RuleFor(x => x.MinRealizedPnL)
            .LessThanOrEqualTo(x => x.MaxRealizedPnL)
            .When(x => x.MinRealizedPnL.HasValue && x.MaxRealizedPnL.HasValue);

            RuleFor(x => x.FromOpenedAt)
                .LessThanOrEqualTo(x => x.ToOpenedAt)
                .When(x => x.FromOpenedAt.HasValue && x.ToOpenedAt.HasValue);

            RuleFor(x => x.FromClosedAt)
                .LessThanOrEqualTo(x => x.ToClosedAt)
                .When(x => x.FromClosedAt.HasValue && x.ToClosedAt.HasValue);

            RuleFor(x => x.SearchTerm)
                .MaximumLength(100);
        }
	}
}

