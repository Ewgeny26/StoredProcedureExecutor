using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedureExecutor.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BrightLight");

            migrationBuilder.CreateTable(
                name: "SPExecutorApp_Procedures",
                schema: "BrightLight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Server = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Database = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Schema = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastExecutedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastRefreshedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSentTemplateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailRecipients = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EmailSubject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OutputReportPath = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPExecutorApp_Procedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SPExecutorApp_ProcedureParams",
                schema: "BrightLight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcedureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPExecutorApp_ProcedureParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SPExecutorApp_ProcedureParams_SPExecutorApp_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalSchema: "BrightLight",
                        principalTable: "SPExecutorApp_Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SPExecutorApp_Templates",
                schema: "BrightLight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcedureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPExecutorApp_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SPExecutorApp_Templates_SPExecutorApp_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalSchema: "BrightLight",
                        principalTable: "SPExecutorApp_Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SPExecutorApp_ProcedureParams_ProcedureId",
                schema: "BrightLight",
                table: "SPExecutorApp_ProcedureParams",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_SPExecutorApp_Templates_ProcedureId",
                schema: "BrightLight",
                table: "SPExecutorApp_Templates",
                column: "ProcedureId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SPExecutorApp_ProcedureParams",
                schema: "BrightLight");

            migrationBuilder.DropTable(
                name: "SPExecutorApp_Templates",
                schema: "BrightLight");

            migrationBuilder.DropTable(
                name: "SPExecutorApp_Procedures",
                schema: "BrightLight");
        }
    }
}
