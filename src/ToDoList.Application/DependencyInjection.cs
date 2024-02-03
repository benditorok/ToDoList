using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Common.Interfaces;
using ToDoList.Application.NoteLists;
using ToDoList.Application.Notes;
using ToDoList.Domain.Entities;

namespace ToDoList.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILogic<Note>, NoteLogic>();
        services.AddScoped<ILogic<NoteList>, NoteListLogic>();

        return services;
    }
}
