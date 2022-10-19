using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_Address_AddressId",
                table: "GameNights");

            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_People_OrganiserId",
                table: "GameNights");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Address_AddressId",
                table: "People");

            migrationBuilder.DropTable(
                name: "GameList");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameNights",
                table: "GameNights");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Person");

            migrationBuilder.RenameTable(
                name: "GameNights",
                newName: "GameNight");

            migrationBuilder.RenameIndex(
                name: "IX_People_AddressId",
                table: "Person",
                newName: "IX_Person_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_GameNights_OrganiserId",
                table: "GameNight",
                newName: "IX_GameNight_OrganiserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameNights_AddressId",
                table: "GameNight",
                newName: "IX_GameNight_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameNight",
                table: "GameNight",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GameNight_Game",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    GameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNight_Game", x => new { x.GameId, x.GameNightId });
                    table.ForeignKey(
                        name: "FK_GameNight_Game_GameNight_GameNightId",
                        column: x => x.GameNightId,
                        principalTable: "GameNight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNight_Game_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameNight_Player",
                columns: table => new
                {
                    GameNightId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNight_Player", x => new { x.GameNightId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_GameNight_Player_GameNight_GameNightId",
                        column: x => x.GameNightId,
                        principalTable: "GameNight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNight_Player_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

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

            migrationBuilder.InsertData(
                table: "GameNight_Game",
                columns: new[] { "GameId", "GameNightId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "GameNight_Player",
                columns: new[] { "GameNightId", "PersonId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_Game_GameNightId",
                table: "GameNight_Game",
                column: "GameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_Player_PersonId",
                table: "GameNight_Player",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNight_Address_AddressId",
                table: "GameNight",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameNight_Person_OrganiserId",
                table: "GameNight",
                column: "OrganiserId",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Address_AddressId",
                table: "Person",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNight_Address_AddressId",
                table: "GameNight");

            migrationBuilder.DropForeignKey(
                name: "FK_GameNight_Person_OrganiserId",
                table: "GameNight");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Address_AddressId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "GameNight_Game");

            migrationBuilder.DropTable(
                name: "GameNight_Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameNight",
                table: "GameNight");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "People");

            migrationBuilder.RenameTable(
                name: "GameNight",
                newName: "GameNights");

            migrationBuilder.RenameIndex(
                name: "IX_Person_AddressId",
                table: "People",
                newName: "IX_People_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_GameNight_OrganiserId",
                table: "GameNights",
                newName: "IX_GameNights_OrganiserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameNight_AddressId",
                table: "GameNights",
                newName: "IX_GameNights_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameNights",
                table: "GameNights",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GameList",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    GameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameList", x => new { x.GameId, x.GameNightId });
                    table.ForeignKey(
                        name: "FK_GameList_GameNights_GameNightId",
                        column: x => x.GameNightId,
                        principalTable: "GameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameList_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    GameNightId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => new { x.GameNightId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_Players_GameNights_GameNightId",
                        column: x => x.GameNightId,
                        principalTable: "GameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "GameList",
                columns: new[] { "GameId", "GameNightId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "GameNights",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 10, 16, 14, 14, 27, 154, DateTimeKind.Local).AddTicks(7120));

            migrationBuilder.UpdateData(
                table: "GameNights",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2022, 10, 16, 14, 14, 27, 154, DateTimeKind.Local).AddTicks(7180));

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "GameNightId", "PersonId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameList_GameNightId",
                table: "GameList",
                column: "GameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PersonId",
                table: "Players",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_Address_AddressId",
                table: "GameNights",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_People_OrganiserId",
                table: "GameNights",
                column: "OrganiserId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Address_AddressId",
                table: "People",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
