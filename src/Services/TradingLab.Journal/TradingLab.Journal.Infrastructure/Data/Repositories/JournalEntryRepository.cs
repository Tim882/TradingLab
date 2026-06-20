using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Infrastructure.Data.Repositories
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly JournalDbContext _dbContext;
        private readonly DbSet<JournalEntry> _dbSet;

        public JournalEntryRepository(JournalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<JournalEntry>();
        }

        public async Task CreateAsync(JournalEntry entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(JournalEntry entity)
        {
            _dbContext.Set<JournalEntry>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<JournalEntry> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(JournalEntry entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

