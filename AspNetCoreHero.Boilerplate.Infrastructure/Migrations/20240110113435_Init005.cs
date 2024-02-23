using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SubscriptionType");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubscriptionType",
                newName: "SubscriptionDuration");

            migrationBuilder.RenameColumn(
                name: "ImageUri",
                table: "SubscriptionType",
                newName: "SubscriptionStatus");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "SubscriptionType",
                newName: "Subscription");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SubscriptionType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "SubscriptionAmount",
                table: "SubscriptionType",
                type: "float",
                maxLength: 500,
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SubscriptionType");

            migrationBuilder.DropColumn(
                name: "SubscriptionAmount",
                table: "SubscriptionType");

            migrationBuilder.RenameColumn(
                name: "SubscriptionStatus",
                table: "SubscriptionType",
                newName: "ImageUri");

            migrationBuilder.RenameColumn(
                name: "SubscriptionDuration",
                table: "SubscriptionType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Subscription",
                table: "SubscriptionType",
                newName: "Code");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SubscriptionType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
