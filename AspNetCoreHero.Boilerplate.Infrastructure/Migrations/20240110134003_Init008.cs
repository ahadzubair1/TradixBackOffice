using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Withdrawals",
                newName: "Withdrawals",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "WalletTypes",
                newName: "WalletTypes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Wallets",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "VolumeDetails",
                newName: "VolumeDetails",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserSessionActivities",
                newName: "UserSessionActivities",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserRanks",
                newName: "UserRanks",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserNetworkTrees",
                newName: "UserNetworkTrees",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserMemberships",
                newName: "UserMemberships",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserLoginHistory",
                newName: "UserLoginHistory",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserBonuses",
                newName: "UserBonuses",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserActivities",
                newName: "UserActivities",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transactions",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TicketTypes",
                newName: "TicketTypes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Tickets",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TicketComments",
                newName: "TicketComments",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TicketAttachments",
                newName: "TicketAttachments",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "SubscriptionType",
                newName: "SubscriptionType",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Rewards",
                newName: "Rewards",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "RankVolumes",
                newName: "RankVolumes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Ranks",
                newName: "Ranks",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "Purchases",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Platforms",
                newName: "Platforms",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "NetworkVolumes",
                newName: "NetworkVolumes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Networks",
                newName: "Networks",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "MenuItems",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "MembershipTypes",
                newName: "MembershipTypes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Memberships",
                newName: "Memberships",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Languages",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "DirectReferrals",
                newName: "DirectReferrals",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Deposits",
                newName: "Deposits",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Countries",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brand",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "BonusTypes",
                newName: "BonusTypes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                newName: "AuditLogs",
                newSchema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "Withdrawals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "UserRanks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "UserMemberships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "Deposits",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    DefaultDownlinePlacementPosition = table.Column<int>(type: "int", nullable: false),
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KycId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferredBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Networks_NetworkId",
                        column: x => x.NetworkId,
                        principalSchema: "dbo",
                        principalTable: "Networks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_ApplicationUserId",
                schema: "dbo",
                table: "Withdrawals",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRanks_ApplicationUserId",
                schema: "dbo",
                table: "UserRanks",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberships_ApplicationUserId",
                schema: "dbo",
                table: "UserMemberships",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ApplicationUserId",
                schema: "dbo",
                table: "Transactions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ApplicationUserId",
                schema: "dbo",
                table: "Purchases",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_ApplicationUserId",
                schema: "dbo",
                table: "Deposits",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "dbo",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "dbo",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "dbo",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "dbo",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NetworkId",
                schema: "dbo",
                table: "Users",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Users_ApplicationUserId",
                schema: "dbo",
                table: "Deposits",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_ApplicationUserId",
                schema: "dbo",
                table: "Purchases",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_ApplicationUserId",
                schema: "dbo",
                table: "Transactions",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMemberships_Users_ApplicationUserId",
                schema: "dbo",
                table: "UserMemberships",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRanks_Users_ApplicationUserId",
                schema: "dbo",
                table: "UserRanks",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdrawals_Users_ApplicationUserId",
                schema: "dbo",
                table: "Withdrawals",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Users_ApplicationUserId",
                schema: "dbo",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_ApplicationUserId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_ApplicationUserId",
                schema: "dbo",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMemberships_Users_ApplicationUserId",
                schema: "dbo",
                table: "UserMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRanks_Users_ApplicationUserId",
                schema: "dbo",
                table: "UserRanks");

            migrationBuilder.DropForeignKey(
                name: "FK_Withdrawals_Users_ApplicationUserId",
                schema: "dbo",
                table: "Withdrawals");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Withdrawals_ApplicationUserId",
                schema: "dbo",
                table: "Withdrawals");

            migrationBuilder.DropIndex(
                name: "IX_UserRanks_ApplicationUserId",
                schema: "dbo",
                table: "UserRanks");

            migrationBuilder.DropIndex(
                name: "IX_UserMemberships_ApplicationUserId",
                schema: "dbo",
                table: "UserMemberships");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ApplicationUserId",
                schema: "dbo",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ApplicationUserId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Deposits_ApplicationUserId",
                schema: "dbo",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "Withdrawals");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "UserRanks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "UserMemberships");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "dbo",
                table: "Deposits");

            migrationBuilder.RenameTable(
                name: "Withdrawals",
                schema: "dbo",
                newName: "Withdrawals");

            migrationBuilder.RenameTable(
                name: "WalletTypes",
                schema: "dbo",
                newName: "WalletTypes");

            migrationBuilder.RenameTable(
                name: "Wallets",
                schema: "dbo",
                newName: "Wallets");

            migrationBuilder.RenameTable(
                name: "VolumeDetails",
                schema: "dbo",
                newName: "VolumeDetails");

            migrationBuilder.RenameTable(
                name: "UserSessionActivities",
                schema: "dbo",
                newName: "UserSessionActivities");

            migrationBuilder.RenameTable(
                name: "UserRanks",
                schema: "dbo",
                newName: "UserRanks");

            migrationBuilder.RenameTable(
                name: "UserNetworkTrees",
                schema: "dbo",
                newName: "UserNetworkTrees");

            migrationBuilder.RenameTable(
                name: "UserMemberships",
                schema: "dbo",
                newName: "UserMemberships");

            migrationBuilder.RenameTable(
                name: "UserLoginHistory",
                schema: "dbo",
                newName: "UserLoginHistory");

            migrationBuilder.RenameTable(
                name: "UserBonuses",
                schema: "dbo",
                newName: "UserBonuses");

            migrationBuilder.RenameTable(
                name: "UserActivities",
                schema: "dbo",
                newName: "UserActivities");

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "dbo",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "TicketTypes",
                schema: "dbo",
                newName: "TicketTypes");

            migrationBuilder.RenameTable(
                name: "Tickets",
                schema: "dbo",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "TicketComments",
                schema: "dbo",
                newName: "TicketComments");

            migrationBuilder.RenameTable(
                name: "TicketAttachments",
                schema: "dbo",
                newName: "TicketAttachments");

            migrationBuilder.RenameTable(
                name: "SubscriptionType",
                schema: "dbo",
                newName: "SubscriptionType");

            migrationBuilder.RenameTable(
                name: "Rewards",
                schema: "dbo",
                newName: "Rewards");

            migrationBuilder.RenameTable(
                name: "RankVolumes",
                schema: "dbo",
                newName: "RankVolumes");

            migrationBuilder.RenameTable(
                name: "Ranks",
                schema: "dbo",
                newName: "Ranks");

            migrationBuilder.RenameTable(
                name: "Purchases",
                schema: "dbo",
                newName: "Purchases");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "dbo",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Platforms",
                schema: "dbo",
                newName: "Platforms");

            migrationBuilder.RenameTable(
                name: "NetworkVolumes",
                schema: "dbo",
                newName: "NetworkVolumes");

            migrationBuilder.RenameTable(
                name: "Networks",
                schema: "dbo",
                newName: "Networks");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                schema: "dbo",
                newName: "MenuItems");

            migrationBuilder.RenameTable(
                name: "MembershipTypes",
                schema: "dbo",
                newName: "MembershipTypes");

            migrationBuilder.RenameTable(
                name: "Memberships",
                schema: "dbo",
                newName: "Memberships");

            migrationBuilder.RenameTable(
                name: "Languages",
                schema: "dbo",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "DirectReferrals",
                schema: "dbo",
                newName: "DirectReferrals");

            migrationBuilder.RenameTable(
                name: "Deposits",
                schema: "dbo",
                newName: "Deposits");

            migrationBuilder.RenameTable(
                name: "Countries",
                schema: "dbo",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Brand",
                schema: "dbo",
                newName: "Brand");

            migrationBuilder.RenameTable(
                name: "BonusTypes",
                schema: "dbo",
                newName: "BonusTypes");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                schema: "dbo",
                newName: "AuditLogs");
        }
    }
}
