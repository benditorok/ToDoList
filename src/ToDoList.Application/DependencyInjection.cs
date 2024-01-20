using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Logic;

namespace ToDoList.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<NoteLogic>();
        services.AddTransient<NoteListLogic>();

        return services;
    }
}
