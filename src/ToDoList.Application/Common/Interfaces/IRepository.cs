namespace ToDoList.Application.Common.Interfaces;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T item);

    Task<T> ReadAsync(int id);

    Task UpdateAsync(T item);

    Task DeleteAsync(int id);

    IQueryable<T> ReadAll();
}
