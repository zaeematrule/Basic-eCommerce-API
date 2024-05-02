namespace WebApiTemplate.Core;

public interface IReadRepository<T>
    where T : BaseEntity
{
    public Task<T?> GetById(int id);

    Task<PaginatedResponse<T>> GetPaginatedResults(int page, int pageSize);
}
