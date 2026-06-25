using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Domain.Enums;
using TradingLab.Journal.Application.Interfaces.Repositories;

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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<Trade> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Trade entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<Trade>> GetFilteredAsync(
        TradeFilterDto filter,
        CancellationToken ct = default)
        {
            var query = _dbSet
                .Include(t => t.Tags)
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

            return new PaginatedList<Trade>(items, totalCount, filter.PageNumber, filter.PageSize);
        }

        private IQueryable<Trade> ApplyFilters(IQueryable<Trade> query, TradeFilterDto filter)
        {

            if (!string.IsNullOrWhiteSpace(filter.Side))
                query = query.Where(t => t.Side == Enum.Parse<Side>(filter.Side, true));

            if (filter.MinQuantity.HasValue)
                query = query.Where(t => t.Quantity >= filter.MinQuantity.Value);

            if (filter.MaxQuantity.HasValue)
                query = query.Where(t => t.Quantity <= filter.MaxQuantity.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(t => t.ExecutedAt >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(t => t.ExecutedAt <= filter.ToDate.Value);

            if (filter.TradingAccountId.HasValue)
                query = query.Where(t => t.TradingAccountId == filter.TradingAccountId.Value);

            if (filter.TagIds?.Any() == true)
            {
                foreach (var tagId in filter.TagIds)
                {
                    query = query.Where(t => t.Tags.Any(tag => tag.Id == tagId));
                }
            }

            return query;
        }

        private IQueryable<Trade> ApplySearch(IQueryable<Trade> query, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            return query.Where(t => t.Notes.Any(n => n.Content.Contains(searchTerm)));
        }

        private IQueryable<Trade> ApplySorting(
            IQueryable<Trade> query,
            string? sortBy,
            bool sortDescending)
        {
            query = sortBy?.ToLower() switch
            {
                "amount" => sortDescending
                    ? query.OrderByDescending(t => t.Quantity)
                    : query.OrderBy(t => t.Quantity),

                "price" => sortDescending
                    ? query.OrderByDescending(t => t.Price)
                    : query.OrderBy(t => t.Price),

                _ => sortDescending
                    ? query.OrderByDescending(t => t.ExecutedAt)
                    : query.OrderBy(t => t.ExecutedAt)
            };

            return query;
        }
    }
}

