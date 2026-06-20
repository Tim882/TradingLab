using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Infrastructure.Data.Repositories
{
    public class TradingAccountRepository : ITradingAccountRepository
    {
        private readonly JournalDbContext _dbContext;
        private readonly DbSet<TradingAccount> _dbSet;

        public TradingAccountRepository(JournalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TradingAccount>();
        }

        public async Task CreateAsync(TradingAccount entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TradingAccount entity)
        {
            _dbContext.Set<TradingAccount>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TradingAccount> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(TradingAccount entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

