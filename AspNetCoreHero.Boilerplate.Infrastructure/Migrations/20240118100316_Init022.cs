using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "SubscriptionType");

            migrationBuilder.DropColumn(
                name: "SubscriptionDuration",
                schema: "dbo",
                table: "SubscriptionType");

            migrationBuilder.RenameColumn(
                name: "SubscriptionStatus",
                schema: "dbo",
                table: "SubscriptionType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SubscriptionName",
                schema: "dbo",
                table: "SubscriptionType",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "SubscriptionAmount",
                schema: "dbo",
                table: "SubscriptionType",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                schema: "dbo",
                table: "SubscriptionType",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationInterval",
                schema: "dbo",
                table: "SubscriptionType",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                schema: "dbo",
                table: "SubscriptionType");

            migrationBuilder.DropColumn(
                name: "DurationInterval",
                schema: "dbo",
                table: "SubscriptionType");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "SubscriptionType",
                newName: "SubscriptionStatus");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "dbo",
                table: "SubscriptionType",
                newName: "SubscriptionName");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "dbo",
                table: "SubscriptionType",
                newName: "SubscriptionAmount");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "SubscriptionType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionDuration",
                schema: "dbo",
                table: "SubscriptionType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
