using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init904 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "IsActive",
                schema: "dbo",
                table: "Users",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
