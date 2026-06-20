using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Infrastructure.Data.Repositories
{
    public class TradeNoteRepository : ITradeNoteRepository
    {
        private readonly JournalDbContext _dbContext;
        private readonly DbSet<TradeNote> _dbSet;

        public TradeNoteRepository(JournalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TradeNote>();
        }

        public async Task CreateAsync(TradeNote entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TradeNote entity)
        {
            _dbContext.Set<TradeNote>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TradeNote> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(TradeNote entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

