using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Infrastructure.Data.Configurations;

internal class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        var hasher = new PasswordHasher<IdentityUser>();

        builder
            .HasData(new IdentityUser[]
            {
                // ManagerOne
                new IdentityUser()
                {
                    Id = "DA6DCE78-ADCA-478C-89B3-76923A4795DE",
                    UserName = "ManagerOne",
                    NormalizedUserName = "MANAGERONE",
                    PasswordHash = hasher.HashPassword(null!, "Passw0rd!")
                },

                // ManagerTwo
                new IdentityUser()
                {
                    Id = "A4EBC743-0CBE-4076-8E93-1EF965DF809E",
                    UserName = "ManagerOne",
                    NormalizedUserName = "MANAGERONE",
                    PasswordHash = hasher.HashPassword(null!, "Passw0rd!")
                },

                // Administrator
                new IdentityUser()
                {
                    Id = "D0AAFECD-06BD-43F9-B71E-79B6A63BF5A6",
                    UserName = "Administrator",
                    NormalizedUserName = "ADMINISTRATOR",
                    PasswordHash = hasher.HashPassword(null!, "Passw0rd!")
                }
            });

    }
}
