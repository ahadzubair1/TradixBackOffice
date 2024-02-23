using AspNetCoreHero.Boilerplate.Application.Interfaces.Contexts;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using VM.WebApi.Domain.App;
using static AspNetCoreHero.Boilerplate.Application.Constants.Permissions;

namespace AspNetCoreHero.Boilerplate.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableIdentityContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<MiningRaceEvents> Event { get; set; }



        //public DbSet<Events> Eventss => Set<Events>();
        //public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<BonusType> BonusTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<NetworkVolume> NetworkVolumes { get; set; }
        public DbSet<RankVolume> RankVolumes { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<UserBonus> UserBonuses { get; set; }
        public DbSet<DirectReferral> DirectReferrals { get; set; }
        public DbSet<UserMembership> UserMemberships { get; set; }
        public DbSet<UserRank> UserRanks { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public DbSet<VolumeDetails> VolumeDetails { get; set; }
        public DbSet<UserNetworkTree> UserNetworkTrees { get; set; }
        public DbSet<UserSessionActivity> UserSessionActivities { get; set; }
        public DbSet<UserPurchasedSubscriptions> UserPurchasedSubscriptions { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<ReferralCode> ReferralCodes { get; set; }



        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var userId = _authenticatedUser == null || string.IsNullOrEmpty(_authenticatedUser.UserId) ? Guid.Empty : Guid.Parse(_authenticatedUser.UserId);

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        //entry.Entity.CreatedBy = userId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        //entry.Entity.LastModifiedBy = userId;
                        break;
                }
            }
            if (userId == Guid.Empty)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(userId.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            //Unique constraint on Code, so it will always be unique.
            builder.Entity<Ticket>().HasIndex(u => u.Code).IsUnique();

            //base.OnModelCreating(builder);
        }
    }
}