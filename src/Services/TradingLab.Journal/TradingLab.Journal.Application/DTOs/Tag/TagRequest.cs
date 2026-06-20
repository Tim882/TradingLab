using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.DTOs
{
	public class TagRequest
    {
		public string Name { get; set; } = string.Empty;

		public Tag ToEntity(Guid id)
		{
			Tag entity = new Tag
			{
				Id = id,
				Name = Name
			};

			return entity;
		}
	}
}

