using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Domain.Interfaces.Repositories;

namespace TradingLab.Journal.Infrastructure.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly JournalDbContext _dbContext;
        private readonly DbSet<Tag> _dbSet;

        public TagRepository(JournalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Tag>();
        }

        public async Task CreateAsync(Tag entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tag entity)
        {
            _dbContext.Set<Tag>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Tag entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

