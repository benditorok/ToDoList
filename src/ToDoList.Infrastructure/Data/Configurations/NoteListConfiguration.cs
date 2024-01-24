using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Data.Configurations;

internal class NoteListConfiguration : IEntityTypeConfiguration<NoteList>
{
    public void Configure(EntityTypeBuilder<NoteList> builder)
    {
        // Primary key
        builder.HasKey(x => x.Id);

        // Auto increment
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        // Navigation property
        builder
            .HasMany(x => x.Notes)
            .WithOne(x => x.NoteList)
            .HasForeignKey(x => x.NoteListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
