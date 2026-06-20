using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Domain.Enums;

namespace TradingLab.Journal.Application.DTOs
{
	public class TagResponse : BaseResponse
    {
		public string Name { get; set; } = string.Empty;

        public TagResponse(Tag entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}

