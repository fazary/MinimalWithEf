namespace MinimalWithEf.Data;

public interface IRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity, CancellationToken ct);
    Task AddAsync(IEnumerable<T> entities, CancellationToken ct);
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T?>> GetAllAsync();
    Task UpdateAsync(T? entity,CancellationToken ct);
    Task<int> DeletAsync(int id,CancellationToken ct);
    Task SaveAsync(CancellationToken ct);
}