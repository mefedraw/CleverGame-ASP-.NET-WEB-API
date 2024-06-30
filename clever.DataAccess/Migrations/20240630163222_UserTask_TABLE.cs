using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clever.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserTask_TABLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbUserTask",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    TgId = table.Column<string>(type: "character varying(30)", nullable: false),
                    TaskId = table.Column<short>(type: "smallint", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbUserTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbUserTask_DbTasksInfo_TaskId",
                        column: x => x.TaskId,
                        principalTable: "DbTasksInfo",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbUserTask_DbUserAuth_TgId",
                        column: x => x.TgId,
                        principalTable: "DbUserAuth",
                        principalColumn: "TgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbUserTask_TaskId",
                table: "DbUserTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DbUserTask_TgId",
                table: "DbUserTask",
                column: "TgId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbUserTask");
        }
    }
}
