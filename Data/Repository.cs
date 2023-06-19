namespace MinimalWithEf.Data;
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _db;
    public Repository(AppDbContext db)
    {
        _db = db;
    }
    public async Task AddAsync(T entity, CancellationToken ct)
    {
        await _db.Set<T>().AddAsync(entity, ct);
        await SaveAsync(ct);
    }
    public async Task AddAsync(IEnumerable<T> entities, CancellationToken ct)
    {
        await _db.Set<T>().AddRangeAsync(entities, ct);
        await SaveAsync(ct);
    }
    public async Task<T?> GetByIdAsync(int id) => await _db.Set<T>().AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id);
    public async Task<IReadOnlyList<T?>> GetAllAsync() => await _db.Set<T>().ToListAsync();

    public async Task UpdateAsync(T? entity, CancellationToken ct)
    {
        _db.Set<T>().Entry(entity!).State = EntityState.Modified;
        await SaveAsync(ct);
    }
    public async Task<int> DeletAsync(int id, CancellationToken ct) =>
        await _db.Set<T>().Where(model => model.Id == id)
                .ExecuteDeleteAsync();
    public async Task SaveAsync(CancellationToken ct) => await _db.SaveChangesAsync(ct);
}