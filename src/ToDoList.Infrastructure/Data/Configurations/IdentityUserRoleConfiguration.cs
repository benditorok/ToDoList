using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Infrastructure.Data.Configurations;

internal class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder
            .HasData(new IdentityUserRole<string>[]
            {
                // ManagerOne
                new IdentityUserRole<string>()
                {
                    UserId = "DA6DCE78-ADCA-478C-89B3-76923A4795DE",
                    RoleId = "192D7504-A2E1-4BC6-8F4B-FB7B53D40558"
                },

                // ManagerTwo
                new IdentityUserRole<string>()
                {
                    UserId = "A4EBC743-0CBE-4076-8E93-1EF965DF809E",
                    RoleId = "192D7504-A2E1-4BC6-8F4B-FB7B53D40558"
                },

                // Administrator
                new IdentityUserRole<string>()
                {
                    UserId = "D0AAFECD-06BD-43F9-B71E-79B6A63BF5A6",
                    RoleId = "5407FE7E-7A5C-4A81-8013-5E80BC3C0FA1"
                }
            });
    }
}
