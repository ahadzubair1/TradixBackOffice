using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init0047 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NftId",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlatformSource",
                schema: "dbo",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "dbo",
                table: "UserNetworkTrees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                schema: "dbo",
                table: "UserNetworkTrees",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sponsor",
                schema: "dbo",
                table: "UserNetworkTrees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NftId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PlatformSource",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "dbo",
                table: "UserNetworkTrees");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "dbo",
                table: "UserNetworkTrees");

            migrationBuilder.DropColumn(
                name: "Sponsor",
                schema: "dbo",
                table: "UserNetworkTrees");
        }
    }
}
