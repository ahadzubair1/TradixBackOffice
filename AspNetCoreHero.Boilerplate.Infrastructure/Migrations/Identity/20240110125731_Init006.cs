using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations.Identity
{
    /// <inheritdoc />
    public partial class Init006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Withdrawal",
                schema: "Identity",
                newName: "Withdrawal",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "WalletType",
                schema: "Identity",
                newName: "WalletType",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Wallet",
                schema: "Identity",
                newName: "Wallet",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "VolumeDetails",
                schema: "Identity",
                newName: "VolumeDetails",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "Identity",
                newName: "UserTokens",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Identity",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Identity",
                newName: "UserRoles",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserRank",
                schema: "Identity",
                newName: "UserRank",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserMembership",
                schema: "Identity",
                newName: "UserMembership",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "Identity",
                newName: "UserLogins",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "Identity",
                newName: "UserClaims",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Transaction",
                schema: "Identity",
                newName: "Transaction",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Identity",
                newName: "Roles",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                schema: "Identity",
                newName: "RoleClaims",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Rank",
                schema: "Identity",
                newName: "Rank",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Purchase",
                schema: "Identity",
                newName: "Purchase",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Network",
                schema: "Identity",
                newName: "Network",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "MembershipTypes",
                schema: "Identity",
                newName: "MembershipTypes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Membership",
                schema: "Identity",
                newName: "Membership",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Deposit",
                schema: "Identity",
                newName: "Deposit",
                newSchema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.RenameTable(
                name: "Withdrawal",
                schema: "dbo",
                newName: "Withdrawal",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "WalletType",
                schema: "dbo",
                newName: "WalletType",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Wallet",
                schema: "dbo",
                newName: "Wallet",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "VolumeDetails",
                schema: "dbo",
                newName: "VolumeDetails",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "dbo",
                newName: "UserTokens",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "Users",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "dbo",
                newName: "UserRoles",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "UserRank",
                schema: "dbo",
                newName: "UserRank",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "UserMembership",
                schema: "dbo",
                newName: "UserMembership",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "dbo",
                newName: "UserLogins",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "dbo",
                newName: "UserClaims",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Transaction",
                schema: "dbo",
                newName: "Transaction",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "dbo",
                newName: "Roles",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                schema: "dbo",
                newName: "RoleClaims",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Rank",
                schema: "dbo",
                newName: "Rank",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Purchase",
                schema: "dbo",
                newName: "Purchase",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Network",
                schema: "dbo",
                newName: "Network",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "MembershipTypes",
                schema: "dbo",
                newName: "MembershipTypes",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Membership",
                schema: "dbo",
                newName: "Membership",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Deposit",
                schema: "dbo",
                newName: "Deposit",
                newSchema: "Identity");
        }
    }
}
