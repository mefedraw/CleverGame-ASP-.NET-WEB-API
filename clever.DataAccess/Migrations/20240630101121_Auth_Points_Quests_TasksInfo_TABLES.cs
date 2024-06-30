using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace clever.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Auth_Points_Quests_TasksInfo_TABLES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbTasksInfo",
                columns: table => new
                {
                    TaskId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Profit = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Workload = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTasksInfo", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "DbUserAuth",
                columns: table => new
                {
                    TgId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    TgUsername = table.Column<string>(type: "text", nullable: false),
                    AuthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbUserAuth", x => x.TgId);
                });

            migrationBuilder.CreateTable(
                name: "DbPoints",
                columns: table => new
                {
                    TgId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Points = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPoints", x => x.TgId);
                    table.ForeignKey(
                        name: "FK_DbPoints_DbUserAuth_TgId",
                        column: x => x.TgId,
                        principalTable: "DbUserAuth",
                        principalColumn: "TgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbQuests",
                columns: table => new
                {
                    TgId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Completed = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbQuests", x => x.TgId);
                    table.ForeignKey(
                        name: "FK_DbQuests_DbUserAuth_TgId",
                        column: x => x.TgId,
                        principalTable: "DbUserAuth",
                        principalColumn: "TgId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbPoints");

            migrationBuilder.DropTable(
                name: "DbQuests");

            migrationBuilder.DropTable(
                name: "DbTasksInfo");

            migrationBuilder.DropTable(
                name: "DbUserAuth");
        }
    }
}
