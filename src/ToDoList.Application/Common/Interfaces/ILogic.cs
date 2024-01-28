namespace ToDoList.Application.Common.Interfaces;

public interface ILogic<T> where T : class
{
    Task<int> CreateAsync(T item);

    Task<T> ReadAsync(int id);

    Task UpdateAsync(T item);

    Task DeleteAsync(int id);

    IQueryable<T> ReadAll();
}
