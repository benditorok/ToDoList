using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Database;

internal class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public virtual DbSet<Note> Notes => Set<Note>();

    public virtual DbSet<NoteList> NoteLists => Set<NoteList>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Note entity

        // Primary key
        modelBuilder.Entity<Note>()
            .HasKey(x => x.Id);

        // Auto increment
        modelBuilder.Entity<Note>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        #endregion Note entity

        #region NoteList entity

        // Primary key
        modelBuilder.Entity<NoteList>()
            .HasKey(x => x.Id);

        // Auto increment
        modelBuilder.Entity<NoteList>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        // Connection between Note and NoteList
        modelBuilder.Entity<NoteList>()
            .HasMany(x => x.Notes)
            .WithOne(x => x.NoteList)
            .HasForeignKey(x => x.NoteListId);

        #endregion NoteList entity

        #region Identity

        // TODO add roles, seed

        #endregion Identity
    }
}