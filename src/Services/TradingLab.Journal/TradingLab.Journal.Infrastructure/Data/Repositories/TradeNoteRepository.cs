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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<TradeNote> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(TradeNote entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<TradeNote>> GetFilteredAsync(
        TradeNoteFilterDto filter,
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

            return new PaginatedList<TradeNote>(items, totalCount, filter.PageNumber, filter.PageSize);
        }

        private IQueryable<TradeNote> ApplyFilters(IQueryable<TradeNote> query, TradeNoteFilterDto filter)
        {

            if (!string.IsNullOrWhiteSpace(filter.Type))
                query = query.Where(t => t.Type == Enum.Parse<TradeNoteType>(filter.Type, true));

            if (filter.FromDate.HasValue)
                query = query.Where(t => t.Date >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(t => t.Date <= filter.ToDate.Value);

            return query;
        }

        private IQueryable<TradeNote> ApplySearch(IQueryable<TradeNote> query, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            return query.Where(t => t.Content.Contains(searchTerm));
        }

        private IQueryable<TradeNote> ApplySorting(
            IQueryable<TradeNote> query,
            string? sortBy,
            bool sortDescending)
        {
            query = sortBy?.ToLower() switch
            {
                _ => sortDescending
                    ? query.OrderByDescending(t => t.Date)
                    : query.OrderBy(t => t.Date)
            };

            return query;
        }
    }
}

