using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Database;

namespace ToDoList.Infrastructure.Repositories;

internal class NoteListRepository : Repository<NoteList>
{
    public NoteListRepository(ApplicationDbContext ctx) : base(ctx)
    {
    }

    public override async Task<NoteList> ReadAsync(int id)
    {
        return await _ctx.NoteLists.FirstOrDefaultAsync(x => x.Id == id) ?? null!;
    }

    public override async Task UpdateAsync(NoteList item)
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