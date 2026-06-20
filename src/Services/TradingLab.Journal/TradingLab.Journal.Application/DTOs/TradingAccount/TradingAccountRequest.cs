using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.DTOs
{
	public class TradingAccountRequest
    {
		public string Name { get; set; } = string.Empty;

		public TradingAccount ToEntity(Guid id)
		{
			TradingAccount entity = new TradingAccount
			{
				Id = id,
				Name = Name
			};

			return entity;
		}
	}
}

