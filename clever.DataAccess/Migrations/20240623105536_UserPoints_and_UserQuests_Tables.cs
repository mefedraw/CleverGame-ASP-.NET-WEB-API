using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clever.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserPoints_and_UserQuests_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TgId = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbQuests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TgId = table.Column<string>(type: "text", nullable: false),
                    Completed = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbQuests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbPoints");

            migrationBuilder.DropTable(
                name: "DbQuests");
        }
    }
}
