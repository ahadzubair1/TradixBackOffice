using AspNetCoreHero.Boilerplate.Application.Enums;
using AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using NanoidDotNet;
using System.Data;
using System.Linq;
using VM.WebApi.Domain.App;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            var userId = NewId.NextGuid();
            NetworkPosition defaultPosition = NetworkPosition.Right;
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                Id = userId.ToString(),
                UserName = "user",
                Email = "basic@gmail.com",
                FirstName = "Basic",
                LastName = "User",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PasswordHash512 = "1975c28d707cd95c6ead5ed87b3afc00a2c9b3c83afb435be221507dcc0bd54fc4df87534d5976a435eed24b19e112fe8e8b7ab0470718cc373ed54beb8d92bc",
                PasswordHashBcrypt = "$2a$10$.5ZlIwCt6St6BmWEJQlHDuyhJbsS6VyyPUoje8miRF1CRDVX9PY.2",
                Position = NetworkPosition.None,
                DefaultDownlinePlacementPosition = defaultPosition,
                IsActive = 1,
                PlatformSource= PlatformSource.IconX
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

                    await AssingWalletsAsync(userId, dbContext);
                    await AssingReferralCodeAsync(userId, dbContext);
                    await AssingNetworkAsync(userId, defaultPosition, dbContext);
                }
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