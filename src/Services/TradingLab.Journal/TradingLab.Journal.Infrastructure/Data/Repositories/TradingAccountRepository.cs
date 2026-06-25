using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Application.Interfaces.Repositories;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;

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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<TradingAccount> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(TradingAccount entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<TradingAccount>> GetFilteredAsync(
        TradingAccountFilterDto filter,
        CancellationToken ct = default)
        {
            var query = _dbSet
                .AsNoTracking()
                .AsQueryable();

            query = ApplySearch(query, filter.SearchTerm);

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(ct);

            return new PaginatedList<TradingAccount>(items, totalCount, filter.PageNumber, filter.PageSize);
        }

        private IQueryable<TradingAccount> ApplySearch(IQueryable<TradingAccount> query, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            return query.Where(t => t.Name.Contains(searchTerm));
        }
    }
}

