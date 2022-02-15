using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedureExecutor.Migrations
{
    public partial class AddExecStatisticModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SPExecutorApp_ExecStatistics",
                schema: "BrightLight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Procedure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeElpsed = table.Column<TimeSpan>(type: "time", nullable: false),
                    ParamsJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPExecutorApp_ExecStatistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SPExecutorApp_ExecStatistics",
                schema: "BrightLight");
        }
    }
}
