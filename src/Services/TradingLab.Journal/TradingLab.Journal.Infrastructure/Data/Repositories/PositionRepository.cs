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
    public class PositionRepository : IPositionRepository
    {
        private readonly JournalDbContext _dbContext;
        private readonly DbSet<Position> _dbSet;

        public PositionRepository(JournalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Position>();
        }

        public async Task CreateAsync(Position entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Position entity)
        {
            _dbContext.Set<Position>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<Position> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Position entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<Position>> GetFilteredAsync(
        PositionFilterDto filter,
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

            return new PaginatedList<Position>(items, totalCount, filter.PageNumber, filter.PageSize);
        }

        private IQueryable<Position> ApplyFilters(IQueryable<Position> query, PositionFilterDto filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Status))
                query = query.Where(t => t.Status == Enum.Parse<Status>(filter.Status, true));


            if (filter.MinTotalEntryQuantity.HasValue)
                query = query.Where(t => t.TotalEntryQuantity >= filter.MinTotalEntryQuantity.Value);

            if (filter.MaxTotalEntryQuantity.HasValue)
                query = query.Where(t => t.TotalEntryQuantity <= filter.MaxTotalEntryQuantity.Value);

            if (filter.MinAverageEntryPrice.HasValue)
                query = query.Where(t => t.AverageEntryPrice >= filter.MinAverageEntryPrice.Value);

            if (filter.MaxAverageEntryPrice.HasValue)
                query = query.Where(t => t.AverageEntryPrice <= filter.MaxAverageEntryPrice.Value);

            if (filter.MinTotalExitQuantity.HasValue)
                query = query.Where(t => t.TotalExitQuantity <= filter.MinTotalExitQuantity.Value);

            if (filter.MaxTotalExitQuantity.HasValue)
                query = query.Where(t => t.TotalExitQuantity <= filter.MaxTotalExitQuantity.Value);

            if (filter.MinAverageExitPrice.HasValue)
                query = query.Where(t => t.AverageExitPrice <= filter.MinAverageExitPrice.Value);

            if (filter.MaxAverageExitPrice.HasValue)
                query = query.Where(t => t.AverageExitPrice <= filter.MaxAverageExitPrice.Value);

            if (filter.MinRealizedPnL.HasValue)
                query = query.Where(t => t.RealizedPnL <= filter.MinRealizedPnL.Value);

            if (filter.MaxRealizedPnL.HasValue)
                query = query.Where(t => t.RealizedPnL <= filter.MaxRealizedPnL.Value);

            if (filter.FromOpenedAt.HasValue)
                query = query.Where(t => t.OpenedAt >= filter.FromOpenedAt.Value);

            if (filter.ToOpenedAt.HasValue)
                query = query.Where(t => t.OpenedAt <= filter.ToOpenedAt.Value);

            if (filter.FromClosedAt.HasValue)
                query = query.Where(t => t.ClosedAt >= filter.FromClosedAt.Value);

            if (filter.ToClosedAt.HasValue)
                query = query.Where(t => t.ClosedAt <= filter.ToClosedAt.Value);

            if (!string.IsNullOrWhiteSpace(filter.Direction))
                query = query.Where(t => t.Direction == Enum.Parse<Direction>(filter.Direction, true));

            return query;
        }

        private IQueryable<Position> ApplySearch(IQueryable<Position> query, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            return query.Where(t => t.Ticker.Contains(searchTerm.ToUpper()));
        }

        private IQueryable<Position> ApplySorting(
            IQueryable<Position> query,
            string? sortBy,
            bool sortDescending)
        {
            query = sortBy?.ToLower() switch
            {

                "totalEntryQuantity" => sortDescending
                    ? query.OrderByDescending(t => t.TotalEntryQuantity)
                    : query.OrderBy(t => t.TotalEntryQuantity),

                "averageEntryPrice" => sortDescending
                    ? query.OrderByDescending(t => t.AverageEntryPrice)
                    : query.OrderBy(t => t.AverageEntryPrice),

                "totalExitQuantity" => sortDescending
                    ? query.OrderByDescending(t => t.TotalExitQuantity)
                    : query.OrderBy(t => t.TotalExitQuantity),

                "averageExitPrice" => sortDescending
                    ? query.OrderByDescending(t => t.AverageExitPrice)
                    : query.OrderBy(t => t.AverageExitPrice),

                "realizedPnL" => sortDescending
                    ? query.OrderByDescending(t => t.RealizedPnL)
                    : query.OrderBy(t => t.RealizedPnL),

                "openedAt" => sortDescending
                    ? query.OrderByDescending(t => t.OpenedAt)
                    : query.OrderBy(t => t.OpenedAt),

                _ => sortDescending
                    ? query.OrderByDescending(t => t.ClosedAt)
                    : query.OrderBy(t => t.ClosedAt)
            };

            return query;
        }
    }
}

