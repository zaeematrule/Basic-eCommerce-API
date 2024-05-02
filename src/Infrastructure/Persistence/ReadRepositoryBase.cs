
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Core;

namespace WebApiTemplate.Infrastructure.Persistence;

public abstract class ReadRepositoryBase<T> : IReadRepository<T>
    where T : BaseEntity
{
    protected readonly AppDbContext _db;

    public ReadRepositoryBase(AppDbContext db)
    {
        _db = db;
    }

    public virtual async Task<T?> GetById(int id)
    {
        return await _db.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public virtual async Task<PaginatedResponse<T>> GetPaginatedResults(int page, int pageSize)
    {
        var queryable = _db.Set<T>().AsQueryable();
        var count = await queryable.CountAsync();

        var results = await queryable.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResponse<T>(count, results);

    }
}
