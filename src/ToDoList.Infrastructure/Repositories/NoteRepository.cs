using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Database;

namespace ToDoList.Infrastructure.Repositories;

internal class NoteRepository : Repository<Note>
{
    public NoteRepository(ApplicationDbContext ctx) : base(ctx)
    {
    }

    public override async Task<Note> ReadAsync(int id)
    {
        return await _ctx.Notes.FirstOrDefaultAsync(x => x.Id == id) ?? null!;
    }

    public override async Task UpdateAsync(Note item)
    {
        var old = await ReadAsync(item.Id);

        foreach (PropertyInfo prop in old.GetType().GetProperties())
        {
            if (prop.GetAccessors().FirstOrDefault(x => x.IsVirtual) == null)
            {
                prop.SetValue(old, prop.GetValue(item));
            }
        }

        await _ctx.SaveChangesAsync();
    }
}