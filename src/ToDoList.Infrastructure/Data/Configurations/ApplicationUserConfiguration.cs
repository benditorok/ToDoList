using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Identity;

namespace ToDoList.Infrastructure.Data.Configurations;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }
}
