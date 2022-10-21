using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 10, 21, 9, 6, 45, 932, DateTimeKind.Local).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2022, 10, 21, 9, 6, 45, 932, DateTimeKind.Local).AddTicks(3480));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
