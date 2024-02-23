using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init0045 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHashBcrypt",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionType",
                schema: "dbo",
                table: "UserNetworkTrees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHashBcrypt",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                schema: "dbo",
                table: "UserNetworkTrees");
        }
    }
}
