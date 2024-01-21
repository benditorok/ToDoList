using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Common.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Database;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((optionsBuilder) =>
        {
            optionsBuilder
                .UseInMemoryDatabase("todolist")
                .UseLazyLoadingProxies();
        });

        services.AddTransient<IRepository<Note>, NoteRepository>();
        services.AddTransient<IRepository<NoteList>, NoteListRepository>();

        return services;
    }
}