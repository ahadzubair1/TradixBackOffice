using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init0035 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionId",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                schema: "dbo",
                table: "SubscriptionTypes",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                schema: "dbo",
                table: "SubscriptionTypes");

            migrationBuilder.AlterColumn<string>(
                name: "SubscriptionId",
                schema: "dbo",
                table: "UserPurchasedSubscriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
