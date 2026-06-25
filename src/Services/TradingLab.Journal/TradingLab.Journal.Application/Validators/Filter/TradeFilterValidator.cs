using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class TradeFilterValidator : AbstractValidator<TradeFilterDto>
	{
		private static readonly string[] AllowedSortColumns = {  };

        private static readonly string[] ValidSides = {  };

        public TradeFilterValidator()
		{
            Include(new PaginationValidator());

            When(x => !string.IsNullOrEmpty(x.SortBy), () =>
            {
                RuleFor(x => x.SortBy)
                    .Must(x => AllowedSortColumns.Contains(x))
                    .WithMessage($"Sort column must be one of: {string.Join(", ", AllowedSortColumns)}");
            });

            When(x => !string.IsNullOrEmpty(x.Side), () =>
            {
                RuleFor(x => x.Side)
                    .Must(x => ValidSides.Contains(x))
                    .WithMessage($"Side must be one of: {string.Join(", ", ValidSides)}");
            });

            RuleFor(x => x.MinQuantity)
                .LessThanOrEqualTo(x => x.MaxQuantity)
                .When(x => x.MinQuantity.HasValue && x.MaxQuantity.HasValue);

            RuleFor(x => x.FromDate)
                .LessThanOrEqualTo(x => x.ToDate)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue);

            RuleFor(x => x.SearchTerm)
                .MaximumLength(100);
        }
	}
}

