using AspNetCoreHero.Boilerplate.Application.Constants;
using AspNetCoreHero.Boilerplate.Application.Enums;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using NanoidDotNet;
using System.Security.Claims;
using VM.WebApi.Domain.App;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                }
            }
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Users");
            await roleManager.AddPermissionClaim(adminRole, "Products");
            await roleManager.AddPermissionClaim(adminRole, "Brands");
        }

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            var userId = NewId.NextGuid();
            NetworkPosition defaultPosition = NetworkPosition.Right;
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                Id = userId.ToString(),
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Imranullah",
                LastName = "Khan",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Position = NetworkPosition.None,
                DefaultDownlinePlacementPosition = defaultPosition,
                IsActive = 1,
                PasswordHash512 = "1975c28d707cd95c6ead5ed87b3afc00a2c9b3c83afb435be221507dcc0bd54fc4df87534d5976a435eed24b19e112fe8e8b7ab0470718cc373ed54beb8d92bc",
                PasswordHashBcrypt = "$2a$10$.5ZlIwCt6St6BmWEJQlHDuyhJbsS6VyyPUoje8miRF1CRDVX9PY.2",
                PlatformSource = PlatformSource.IconX
            };


            
            //Network network = new Network
            //{
            //    UserId = userId,
            //    //ReferredBy = userId,
            //    Position = defaultPosition
            //};

            //defaultUser.Network = network;

            //ReferralCode referralCodeLeft = new ReferralCode
            //{
            //    UserId = userId,
            //    NetworkPosition = NetworkPosition.Left,
            //    Code = Nanoid.Generate()
            //};

            //ReferralCode referralCodeRight = new ReferralCode
            //{
            //    UserId = userId,
            //    NetworkPosition = NetworkPosition.Right,
            //    Code = Nanoid.Generate()
            //};

            //List<ReferralCode> referralCodes = [referralCodeLeft, referralCodeRight];

            //defaultUser.ReferralCodes = referralCodes;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin@1234");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());


                    await AssingWalletsAsync(userId, dbContext);
                    await AssingReferralCodeAsync(userId, dbContext);
                    await AssingNetworkAsync(userId, defaultPosition, dbContext);
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }


        private static async Task AssingWalletsAsync(Guid userId, ApplicationDbContext dbContext)
        {
            var wallets = dbContext.WalletTypes.Where(x => x.AssignOnCreate == true).ToList();
            foreach (var wallet in wallets)
            {
                await dbContext.Wallets.AddAsync(new Wallet
                {
                    UserId = userId,
                    WalletTypeId = wallet.Id,
                    Balance = 0
                });
            }
        }

        private static async Task AssingReferralCodeAsync(Guid userId, ApplicationDbContext dbContext)
        {

            await dbContext.ReferralCodes.AddAsync(new ReferralCode
            {
                UserId = userId,
                Code = Nanoid.Generate(),
                NetworkPosition = NetworkPosition.Left
            });

            await dbContext.ReferralCodes.AddAsync(new ReferralCode
            {
                UserId = userId,
                Code = Nanoid.Generate(),
                NetworkPosition = NetworkPosition.Right
            });
        }
        private static async Task AssingNetworkAsync(Guid userId, NetworkPosition position, ApplicationDbContext dbContext)
        {

            var network = new Network
            {
                Position = position,
                UserId = userId,
                ParentUserId = null,
                ReferredBy = Guid.Empty
            };

            var addedEntity = await dbContext.Networks.AddAsync(network);

        }

    }
}