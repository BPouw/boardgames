using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNight_Game_Games_GameId",
                table: "GameNight_Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameIndex(
                name: "IX_Games_Name",
                table: "Game",
                newName: "IX_Game_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 10, 19, 16, 17, 25, 512, DateTimeKind.Local).AddTicks(3900));

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2022, 10, 19, 16, 17, 25, 512, DateTimeKind.Local).AddTicks(3960));

            migrationBuilder.AddForeignKey(
                name: "FK_GameNight_Game_Game_GameId",
                table: "GameNight_Game",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNight_Game_Game_GameId",
                table: "GameNight_Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_Game_Name",
                table: "Games",
                newName: "IX_Games_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 10, 19, 16, 14, 18, 21, DateTimeKind.Local).AddTicks(6960));

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2022, 10, 19, 16, 14, 18, 21, DateTimeKind.Local).AddTicks(7020));

            migrationBuilder.AddForeignKey(
                name: "FK_GameNight_Game_Games_GameId",
                table: "GameNight_Game",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
