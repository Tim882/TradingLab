using System;
using Microsoft.EntityFrameworkCore.Storage;
using TradingLab.Journal.Domain.Interfaces.Repositories;
using TradingLab.Journal.Infrastructure.Data.Context;

namespace TradingLab.Journal.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JournalDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public IJournalEntryRepository JournalEntryRepository { get; private set; }
        public IPositionRepository PositionRepository { get; private set; }
        public ITagRepository TagRepository { get; private set; }
        public ITradeNoteRepository TradeNoteRepository { get; private set; }
        public ITradeRepository TradeRepository { get; private set; }
        public ITradingAccountRepository TradingAccountRepository { get; private set; }

        public UnitOfWork(JournalDbContext dbContext)
        {
            _dbContext = dbContext;
            JournalEntryRepository = new JournalEntryRepository(_dbContext);
            PositionRepository = new PositionRepository(_dbContext);
            TagRepository = new TagRepository(_dbContext);
            TradeNoteRepository = new TradeNoteRepository(_dbContext);
            TradeRepository = new TradeRepository(_dbContext);
            TradingAccountRepository = new TradingAccountRepository(_dbContext);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction?.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction?.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }
    }
}

