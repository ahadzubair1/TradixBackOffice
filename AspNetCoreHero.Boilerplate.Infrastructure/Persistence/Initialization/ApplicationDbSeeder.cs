using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Initialization;

internal class ApplicationDbSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CustomSeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, CustomSeederRunner seederRunner, ILogger<ApplicationDbSeeder> logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedRolesAsync(dbContext);
        await SeedAdminUserAsync();
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(ApplicationDbContext dbContext)
    {
        //foreach (string roleName in AppRoles.DefaultRoles)
        //{
        //    if (await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
        //        is not ApplicationRole role)
        //    {
        //        // Create the role
        //        _logger.LogInformation("Seeding {role} Role", roleName);
        //        role = new ApplicationRole(roleName, $"{roleName} Role");
        //        await _roleManager.CreateAsync(role);
        //    }

        //    // Assign permissions
        //    if (roleName == AppRoles.Basic)
        //    {
        //        await AssignPermissionsToRoleAsync(dbContext, AppPermissions.Basic, role);
        //    }
        //    else if (roleName == AppRoles.Admin)
        //    {
        //        await AssignPermissionsToRoleAsync(dbContext, AppPermissions.Admin, role);

        //    }
        //}
    }

    //private async Task AssignPermissionsToRoleAsync(ApplicationDbContext dbContext, IReadOnlyList<AppPermission> permissions, IdentityRole role)
    //{
    //    var currentClaims = await _roleManager.GetClaimsAsync(role);
    //    foreach (var permission in permissions)
    //    {
    //        if (!currentClaims.Any(c => c.Type == AppClaims.Permission && c.Value == permission.Name))
    //        {
    //            _logger.LogInformation("Seeding {role} Permission '{permission}'", role.Name, permission.Name);
    //            dbContext.RoleClaims.Add(new ApplicationRoleClaim
    //            {
    //                RoleId = role.Id,
    //                ClaimType = AppClaims.Permission,
    //                ClaimValue = permission.Name,
    //                CreatedBy = "ApplicationDbSeeder"
    //            });
    //            await dbContext.SaveChangesAsync();
    //        }
    //    }
    //}

    private async Task SeedAdminUserAsync()
    {
        //if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == MultitenancyConstants.Root.EmailAddress)
        //    is not ApplicationUser adminUser)
        //{
        //    string adminUserName = $"{MultitenancyConstants.Root.Id.Trim()}.{AppRoles.Admin}".ToLowerInvariant();

        //    NetworkPosition defaultPosition = NetworkPosition.Right;

        //    adminUser = new ApplicationUser
        //    {
        //        FirstName = MultitenancyConstants.Root.Id.Trim().ToLowerInvariant(),
        //        LastName = AppRoles.Admin,
        //        Email = MultitenancyConstants.Root.EmailAddress,
        //        UserName = adminUserName,
        //        EmailConfirmed = true,
        //        PhoneNumberConfirmed = true,
        //        NormalizedEmail = MultitenancyConstants.Root.EmailAddress?.ToUpperInvariant(),
        //        NormalizedUserName = adminUserName.ToUpperInvariant(),
        //        IsActive = true,
        //        DefaultDownlinePlacementPosition = defaultPosition,
        //    };

        //    var userId = Guid.Parse(adminUser.Id);
        //    Network network = new Network
        //    {
        //        UserId = userId,
        //        //ReferredBy = userId,
        //        Position = defaultPosition
        //    };

        //    adminUser.Network = network;
        //    _logger.LogInformation("Seeding Default Admin User");
        //    var password = new PasswordHasher<ApplicationUser>();
        //    adminUser.PasswordHash = password.HashPassword(adminUser, MultitenancyConstants.DefaultPassword);
        //    await _userManager.CreateAsync(adminUser);
        //}

        //// Assign role to user
        //if (!await _userManager.IsInRoleAsync(adminUser, AppRoles.Admin))
        //{
        //    _logger.LogInformation("Assigning Admin Role to Admin User");
        //    await _userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
        //}
    }
}