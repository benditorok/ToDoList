using ToDoList.Application;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Identity;
using ToDoList.WebApi;
using ToDoList.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddWebServices();

builder.Services.AddLogging();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapIdentityApi<ApplicationUser>();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Initialize the database
await app.InitialiseDatabaseAsync();

app.Run();
