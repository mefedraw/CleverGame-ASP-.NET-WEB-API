using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clever.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FriendShip_TABLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbFriendships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FriendId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FriendRequestAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    FriendsDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbFriendships", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbFriendships");
        }
    }
}
