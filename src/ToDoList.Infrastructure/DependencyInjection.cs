using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Common.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Database;
using ToDoList.Infrastructure.Identity;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Configuring the database
        services.AddDbContext<ApplicationDbContext>((optionsBuilder) =>
        {
            string? postgreConnStr = Environment.GetEnvironmentVariable("POSTGRE_TODOLIST") ?? 
                Environment.GetEnvironmentVariable("POSTGRE_TODOLIST", EnvironmentVariableTarget.User);

            if (!string.IsNullOrWhiteSpace(postgreConnStr))
            {
                optionsBuilder
                    .UseNpgsql(postgreConnStr, x => x.MigrationsAssembly("ToDoList.Infrastructure"))
                    .UseLazyLoadingProxies();
            }
            else 
            {
                optionsBuilder
                    .UseInMemoryDatabase("todolist")
                    .UseLazyLoadingProxies();
            }
        });

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddSingleton(TimeProvider.System);

        // Repositories
        services.AddScoped<IRepository<Note>, NoteRepository>();
        services.AddScoped<IRepository<NoteList>, NoteListRepository>();

        return services;
    }
}