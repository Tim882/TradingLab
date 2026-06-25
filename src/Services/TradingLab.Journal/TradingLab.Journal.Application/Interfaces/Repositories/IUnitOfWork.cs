using System;
using TradingLab.Journal.Domain.Entities;

namespace TradingLab.Journal.Application.Interfaces.Repositories
{
	public interface IUnitOfWork: IDisposable
    {
		public IJournalEntryRepository JournalEntryRepository { get; }
        public IPositionRepository PositionRepository { get; }
        public ITagRepository TagRepository { get; }
        public ITradeNoteRepository TradeNoteRepository { get; }
        public ITradeRepository TradeRepository { get; }
        public ITradingAccountRepository TradingAccountRepository { get; }
        public Task BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollbackTransactionAsync();
    }
}

