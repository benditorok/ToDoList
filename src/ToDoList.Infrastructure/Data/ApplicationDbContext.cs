using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Database;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    protected internal virtual DbSet<Note> Notes => Set<Note>();

    protected internal virtual DbSet<NoteList> NoteLists => Set<NoteList>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}