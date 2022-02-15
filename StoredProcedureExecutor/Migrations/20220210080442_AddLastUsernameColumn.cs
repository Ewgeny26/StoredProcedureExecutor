using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedureExecutor.Migrations
{
    public partial class AddLastUsernameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastUsername",
                schema: "BrightLight",
                table: "SPExecutorApp_Procedures",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUsername",
                schema: "BrightLight",
                table: "SPExecutorApp_Procedures");
        }
    }
}
