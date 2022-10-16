using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdultsOnly",
                table: "GameNights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "GameNights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "MaxPlayers", "Name" },
                values: new object[] { new DateTime(2022, 10, 16, 14, 14, 27, 154, DateTimeKind.Local).AddTicks(7120), 6, "MarioKart session" });

            migrationBuilder.InsertData(
                table: "GameNights",
                columns: new[] { "Id", "AddressId", "AdultsOnly", "DateTime", "MaxPlayers", "Name", "OrganiserId" },
                values: new object[] { 2, 3, true, new DateTime(2022, 10, 16, 14, 14, 27, 154, DateTimeKind.Local).AddTicks(7180), 6, "Poker night", 2 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "genre",
                value: 10);

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AdultsOnly", "Description", "Name", "category", "genre" },
                values: new object[,]
                {
                    { 2, false, "Can you buy all the houses?", "Monopoly", 1, 6 },
                    { 3, false, "Scribble, Scrubble, Scrabble", "Scrabble", 1, 7 },
                    { 4, true, "How offensive can you get?", "Cards against humanity", 0, 3 },
                    { 5, true, "Nice hand bro", "Poker", 0, 7 },
                    { 6, true, "Dont hit at 21", "Blackjack", 0, 7 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameNights",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "AdultsOnly",
                table: "GameNights");

            migrationBuilder.UpdateData(
                table: "GameNights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "MaxPlayers", "Name" },
                values: new object[] { new DateTime(2022, 10, 10, 20, 51, 27, 37, DateTimeKind.Local).AddTicks(2430), 2, "MarioKart" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "genre",
                value: 6);
        }
    }
}
