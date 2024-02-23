using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init0040 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionName",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                newName: "SubscriptionType");

            migrationBuilder.AddColumn<long>(
                name: "PurchasedAt",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndDate",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "SubscriptionEndsAt",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SubscriptionExpiry",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpiryDate",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionPurchaseDate",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionStartDate",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "SubscriptionStartsAt",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasedAt",
                schema: "dbo",
                table: "UserPurchasedSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndDate",
                schema: "dbo",
                table: "UserPurchasedSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndsAt",
                schema: "dbo",
                table: "UserPurchasedSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionExpiry",
                schema: "dbo",
                table: "UserPurchasedSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionExpiryDate",
                schema: "dbo",
                table: "UserPurchasedSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPurchaseDate",
                schema: "dbo",
                table: "UserPurchasedSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionStartDate",
                schema: "dbo",
                table: "UserPurchasedSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionStartsAt",
                schema: "dbo",
                table: "UserPurchasedSubscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionType",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                newName: "SubscriptionName");
        }
    }
}
