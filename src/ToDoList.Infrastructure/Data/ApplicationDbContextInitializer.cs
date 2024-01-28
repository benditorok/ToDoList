using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoList.Domain.Constants;
using ToDoList.Infrastructure.Database;
using ToDoList.Infrastructure.Identity;

namespace ToDoList.Infrastructure.Data;

internal class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var roleAdministrator = new IdentityRole(Roles.Administrator);
        var roleManager = new IdentityRole(Roles.Manager);

        if (_roleManager.Roles.All(r => r.Name != roleAdministrator.Name))
        {
            await _roleManager.CreateAsync(roleAdministrator);
        }

        if (_roleManager.Roles.All(r => r.Name != roleManager.Name))
        {
            await _roleManager.CreateAsync(roleManager);
        }

        // Default users
        var userAdministrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };
        var userManagerOne = new ApplicationUser { UserName = "managerone@localhost", Email = "managerone@localhost" };
        var userManagerTwo = new ApplicationUser { UserName = "managertwo@localhost", Email = "managertwo@localhost" };

        if (_userManager.Users.All(u => u.UserName != userAdministrator.UserName))
        {
            await _userManager.CreateAsync(userAdministrator, "Passw0rd!");

            if (!string.IsNullOrWhiteSpace(roleAdministrator.Name))
            {
                await _userManager.AddToRolesAsync(userAdministrator, new[] { roleAdministrator.Name });
            }
        }


        if (_userManager.Users.All(u => u.UserName != userManagerOne.UserName))
        {
            await _userManager.CreateAsync(userManagerOne, "Passw0rd!");

            if (!string.IsNullOrWhiteSpace(roleManager.Name))
            {
                await _userManager.AddToRolesAsync(userManagerOne, new[] { roleManager.Name });
            }
        }

        if (_userManager.Users.All(u => u.UserName != userManagerTwo.UserName))
        {
            await _userManager.CreateAsync(userManagerTwo, "Passw0rd!");

            if (!string.IsNullOrWhiteSpace(roleManager.Name))
            {
                await _userManager.AddToRolesAsync(userManagerTwo, new[] { roleManager.Name });
            }
        }
    }
}
