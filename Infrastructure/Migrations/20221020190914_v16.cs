using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlcoholFree",
                table: "Person",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LactoseIntolerant",
                table: "Person",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NutAllergy",
                table: "Person",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vegan",
                table: "Person",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AlcoholFree",
                table: "GameNight",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LactoseIntolerant",
                table: "GameNight",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NutAllergy",
                table: "GameNight",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vegan",
                table: "GameNight",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1,
                column: "NutAllergy",
                value: true);

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 2,
                column: "Vegan",
                value: true);

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AlcoholFree", "LactoseIntolerant", "NutAllergy", "Vegan" },
                values: new object[] { true, true, true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlcoholFree",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LactoseIntolerant",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "NutAllergy",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Vegan",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "AlcoholFree",
                table: "GameNight");

            migrationBuilder.DropColumn(
                name: "LactoseIntolerant",
                table: "GameNight");

            migrationBuilder.DropColumn(
                name: "NutAllergy",
                table: "GameNight");

            migrationBuilder.DropColumn(
                name: "Vegan",
                table: "GameNight");

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 10, 20, 18, 49, 9, 591, DateTimeKind.Local).AddTicks(1128));

            migrationBuilder.UpdateData(
                table: "GameNight",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2022, 10, 20, 18, 49, 9, 591, DateTimeKind.Local).AddTicks(1164));
        }
    }
}
