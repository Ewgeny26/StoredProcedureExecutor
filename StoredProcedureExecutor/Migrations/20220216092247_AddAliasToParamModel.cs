using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedureExecutor.Migrations
{
    public partial class AddAliasToParamModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alias",
                schema: "BrightLight",
                table: "SPExecutorApp_ProcedureParams",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
            migrationBuilder.Sql("UPDATE BrightLight.SPExecutorApp_ProcedureParams SET Alias = Name WHERE 1=1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                schema: "BrightLight",
                table: "SPExecutorApp_ProcedureParams");
        }
    }
}
