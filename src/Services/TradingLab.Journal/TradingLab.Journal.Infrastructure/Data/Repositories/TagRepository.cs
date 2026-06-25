using System;
using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Domain.Entities;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Application.Interfaces.Repositories;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;

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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Tag entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<Tag>> GetFilteredAsync(
        TagFilterDto filter,
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

            return new PaginatedList<Tag>(items, totalCount, filter.PageNumber, filter.PageSize);
        }

        private IQueryable<Tag> ApplySearch(IQueryable<Tag> query, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            return query.Where(t => t.Name.Contains(searchTerm));
        }
    }
}

