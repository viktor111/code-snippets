public abstract class GenericRepository<T>: IRepository<T>
    where T: Entity
{
    private readonly AppDbContext _dbContext;

    protected GenericRepository(IServiceScope scope)
    {
        _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
    
    public virtual T Add(T entity) 
        => _dbContext.Add(entity).Entity;

    public virtual async Task<T> FindOne(Expression<Func<T, bool>> predicate)
        => await _dbContext
            .Set<T>()
            .AsQueryable()
            .FirstOrDefaultAsync(predicate) ?? throw new NullReferenceException();

    public virtual async Task<IEnumerable<T>> FindMany(Expression<Func<T, bool>> predicate) 
        => await _dbContext
            .Set<T>()
            .AsQueryable()
            .Where(predicate)
            .ToListAsync();

    public virtual T Delete(T entity)
        => _dbContext.Remove(entity).Entity;

    public virtual async Task<List<T>> GetAll()
        => await _dbContext
            .Set<T>()
            .ToListAsync();

    public Task<bool> Exists(Expression<Func<T, bool>> predicate)
        => _dbContext
            .Set<T>()
            .AnyAsync(predicate);
    
    public async Task<bool> SaveChanges()
        => (await _dbContext.SaveChangesAsync()) > 0;
}