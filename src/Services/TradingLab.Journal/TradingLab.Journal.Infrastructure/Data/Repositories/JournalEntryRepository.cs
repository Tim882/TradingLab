using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Application.Interfaces.Repositories;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Enums;

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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<JournalEntry> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(JournalEntry entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<JournalEntry>> GetFilteredAsync(
        JournalEntryFilterDto filter,
        CancellationToken ct = default)
        {
            var query = _dbSet
                .AsNoTracking()
                .AsQueryable();

            query = ApplyFilters(query, filter);

            query = ApplySearch(query, filter.SearchTerm);

            var totalCount = await query.CountAsync(ct);

            query = ApplySorting(query, filter.SortBy, filter.SortDescending);

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(ct);

            return new PaginatedList<JournalEntry>(items, totalCount, filter.PageNumber, filter.PageSize);
        }

        private IQueryable<JournalEntry> ApplyFilters(IQueryable<JournalEntry> query, JournalEntryFilterDto filter)
        {

            if (!string.IsNullOrWhiteSpace(filter.MarketCondition))
                query = query.Where(t => t.MarketCondition == Enum.Parse<MarketCondition>(filter.MarketCondition, true));

            if (filter.MinMood.HasValue)
                query = query.Where(t => t.Mood >= filter.MinMood.Value);

            if (filter.MaxMood.HasValue)
                query = query.Where(t => t.Mood <= filter.MaxMood.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(t => t.Date >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(t => t.Date <= filter.ToDate.Value);

            return query;
        }

        private IQueryable<JournalEntry> ApplySearch(IQueryable<JournalEntry> query, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            return query.Where(
                t => t.Content.Contains(searchTerm) || t.Title.Contains(searchTerm));
        }

        private IQueryable<JournalEntry> ApplySorting(
            IQueryable<JournalEntry> query,
            string? sortBy,
            bool sortDescending)
        {
            query = sortBy?.ToLower() switch
            {
                "mood" => sortDescending
                    ? query.OrderByDescending(t => t.Mood)
                    : query.OrderBy(t => t.Mood),

                _ => sortDescending
                    ? query.OrderByDescending(t => t.Date)
                    : query.OrderBy(t => t.Date)
            };

            return query;
        }
    }
}

