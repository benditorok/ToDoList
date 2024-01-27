using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

            await initializer.InitialiseAsync();

            await initializer.SeedAsync();
        }
    }
}
