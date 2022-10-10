using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genre = table.Column<int>(type: "int", nullable: false),
                    AdultsOnly = table.Column<bool>(type: "bit", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    NoShows = table.Column<int>(type: "int", nullable: false),
                    Shows = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameNights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    OrganiserId = table.Column<int>(type: "int", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameNights_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNights_People_OrganiserId",
                        column: x => x.OrganiserId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

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
                table: "Address",
                columns: new[] { "Id", "City", "HouseNumber", "PostalCode", "StreetName" },
                values: new object[,]
                {
                    { 1, "Breda", 97, "4814AE", "Tramsingel" },
                    { 2, "Rotterdam", 199, "5317MJ", "Pleinweg" },
                    { 3, "Devtown", 52, "4452SG", "Teststraat" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AdultsOnly", "Description", "Name", "category", "genre" },
                values: new object[] { 1, false, "It's me Mario", "MarioKart", 2, 6 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "AddressId", "DateOfBirth", "Email", "Gender", "Name", "NoShows", "Shows" },
                values: new object[] { 1, 1, new DateTime(1998, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "boris@email.com", 0, "Boris Pouw", 0, 0 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "AddressId", "DateOfBirth", "Email", "Gender", "Name", "NoShows", "Shows" },
                values: new object[] { 2, 2, new DateTime(1999, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ntstefi@email.com", 1, "Stefi Nicoara", 0, 0 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "AddressId", "DateOfBirth", "Email", "Gender", "Name", "NoShows", "Shows" },
                values: new object[] { 3, 3, new DateTime(2000, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "piet@email.com", 0, "Piet Test", 0, 0 });

            migrationBuilder.InsertData(
                table: "GameNights",
                columns: new[] { "Id", "AddressId", "DateTime", "MaxPlayers", "Name", "OrganiserId" },
                values: new object[] { 1, 1, new DateTime(2022, 10, 10, 20, 51, 27, 37, DateTimeKind.Local).AddTicks(2430), 2, "MarioKart", 1 });

            migrationBuilder.InsertData(
                table: "GameList",
                columns: new[] { "GameId", "GameNightId" },
                values: new object[] { 1, 1 });

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
                name: "IX_GameNights_AddressId",
                table: "GameNights",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNights_OrganiserId",
                table: "GameNights",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Name",
                table: "Games",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_AddressId",
                table: "People",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PersonId",
                table: "Players",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameList");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GameNights");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
