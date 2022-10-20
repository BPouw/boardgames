using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class v17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameImageId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PictureFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameImage_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 10, 20, 22, 26, 40, 851, DateTimeKind.Local).AddTicks(4181));

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2022, 10, 20, 22, 26, 40, 851, DateTimeKind.Local).AddTicks(4227));

            migrationBuilder.CreateIndex(
                name: "IX_GameImage_GameID",
                table: "GameImage",
                column: "GameID",
                unique: true,
                filter: "[GameID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameImage");

            migrationBuilder.DropColumn(
                name: "GameImageId",
                table: "Game");

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 10, 20, 21, 9, 14, 20, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2022, 10, 20, 21, 9, 14, 20, DateTimeKind.Local).AddTicks(1925));
        }
    }
}
