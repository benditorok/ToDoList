using ToDoList.Application.Interfaces.Repository;
using ToDoList.Infrastructure.Database;

namespace ToDoList.Infrastructure.Repositories;

internal abstract class Repository<T> : IRepository<T> where T : class
{
    protected ApplicationDbContext _ctx;

    public Repository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task CreateAsync(T item)
    {
        await _ctx.Set<T>().AddAsync(item);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await ReadAsync(id);

        _ctx.Set<T>().Remove(item);
        await _ctx.SaveChangesAsync();
    }

    public IQueryable<T> ReadAll()
    {
        return _ctx.Set<T>().AsQueryable();
    }

    public abstract Task<T> ReadAsync(int id);

    public abstract Task UpdateAsync(T item);
}