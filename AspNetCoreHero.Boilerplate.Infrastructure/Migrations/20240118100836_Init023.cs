using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionType",
                schema: "dbo",
                table: "SubscriptionType");

            migrationBuilder.RenameTable(
                name: "SubscriptionType",
                schema: "dbo",
                newName: "SubscriptionTypes",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionTypes",
                schema: "dbo",
                table: "SubscriptionTypes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionTypes",
                schema: "dbo",
                table: "SubscriptionTypes");

            migrationBuilder.RenameTable(
                name: "SubscriptionTypes",
                schema: "dbo",
                newName: "SubscriptionType",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionType",
                schema: "dbo",
                table: "SubscriptionType",
                column: "Id");
        }
    }
}
