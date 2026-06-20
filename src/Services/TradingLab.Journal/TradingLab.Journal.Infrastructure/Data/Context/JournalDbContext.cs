using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Infrastructure.Data.Context
{
	public class JournalDbContext: DbContext
	{
		public DbSet<JournalEntry> JournalEntries { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<Trade> Trades { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<TradeNote> TradeNotes { get; set; }
		public DbSet<TradingAccount> TradingAccounts { get; set; }

		public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

