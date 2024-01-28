using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.NoteLists;
using ToDoList.Application.Notes;

namespace ToDoList.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<NoteLogic>();
        services.AddScoped<NoteListLogic>();

        return services;
    }
}
