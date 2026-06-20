using System;
using FluentValidation;
using TradingLab.Journal.Application.DTOs;

namespace TradingLab.Journal.Application.Validators
{
	public class TagRequestValidator: AbstractValidator<TagRequest>
    {
		public TagRequestValidator()
		{
			RuleFor(e => e.Name)
				.NotEmpty()
				.WithMessage("Tag must have a name");
		}
	}
}

