﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Data.Configurations;

internal class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        // Primary key
        builder.HasKey(x => x.Id);

        // Auto increment
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}
