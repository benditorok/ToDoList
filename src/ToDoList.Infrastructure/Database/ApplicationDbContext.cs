using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities.ToDo;

namespace ToDoList.Infrastructure.Database;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    protected virtual DbSet<Note>? Notes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    { 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseInMemoryDatabase("todolist")
                .UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Note entity
        // Mark as primary key
        modelBuilder.Entity<Note>()
            .HasKey(x => x.Id);

        // Auto increment
        modelBuilder.Entity<Note>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        #endregion

        #region Identity
        // TODO add roles, seed

        #endregion
    }
}
