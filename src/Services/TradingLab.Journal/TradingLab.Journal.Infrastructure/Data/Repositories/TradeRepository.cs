using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Infrastructure.Data.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly JournalDbContext _dbContext;
        private readonly DbSet<Trade> _dbSet;

        public TradeRepository(JournalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Trade>();
        }

        public async Task CreateAsync(Trade entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Trade entity)
        {
            _dbContext.Set<Trade>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Trade> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Trade entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

