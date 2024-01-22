using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Constants;

namespace ToDoList.Infrastructure.Data.Configurations;

internal class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder
            .HasData(new IdentityRole[]
            {
                // Administrator role
                new IdentityRole()
                {
                    Id = "5407FE7E-7A5C-4A81-8013-5E80BC3C0FA1",
                    Name = Roles.Administrator,
                    NormalizedName = Roles.Administrator.ToUpper()
                },

                // Manager role
                new IdentityRole()
                {
                    Id = "192D7504-A2E1-4BC6-8F4B-FB7B53D40558",
                    Name = Roles.Manager,
                    NormalizedName = Roles.Manager.ToUpper()
                },

                // User role
                new IdentityRole()
                {
                    Id = "4D3DAA13-F455-4A9B-80EC-E63BC0334674",
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper()
                }
            });
    }
}
