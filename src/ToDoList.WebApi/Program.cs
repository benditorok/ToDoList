using Microsoft.AspNetCore.Diagnostics;
using ToDoList.Application;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Identity;
using ToDoList.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddWebServices();

builder.Services.AddLogging();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception handling middleware
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()!.Error;

    var response = new { Msg = exception.Message };
    await context.Response.WriteAsJsonAsync(response);
}));

app.UseHttpsRedirection();

app.MapControllers();

app.MapIdentityApi<ApplicationUser>();

// Initialize the database
await app.InitialiseDatabaseAsync();

app.Run();
