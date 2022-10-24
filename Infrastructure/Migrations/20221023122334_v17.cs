using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class v17 : Migration
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
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genre = table.Column<int>(type: "int", nullable: false),
                    AdultsOnly = table.Column<bool>(type: "bit", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    GameImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Vegan = table.Column<bool>(type: "bit", nullable: false),
                    LactoseIntolerant = table.Column<bool>(type: "bit", nullable: false),
                    NutAllergy = table.Column<bool>(type: "bit", nullable: false),
                    AlcoholFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "GameNight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    OrganiserId = table.Column<int>(type: "int", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    AdultsOnly = table.Column<bool>(type: "bit", nullable: false),
                    Vegan = table.Column<bool>(type: "bit", nullable: false),
                    LactoseIntolerant = table.Column<bool>(type: "bit", nullable: false),
                    NutAllergy = table.Column<bool>(type: "bit", nullable: false),
                    AlcoholFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameNight_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNight_Person_OrganiserId",
                        column: x => x.OrganiserId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Person_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_GameNight_Game_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNight_Game_GameNight_GameNightId",
                        column: x => x.GameNightId,
                        principalTable: "GameNight",
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

            migrationBuilder.CreateTable(
                name: "Person_Review",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person_Review", x => new { x.PersonId, x.ReviewId });
                    table.ForeignKey(
                        name: "FK_Person_Review_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Person_Review_Review_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Review",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                table: "Game",
                columns: new[] { "Id", "AdultsOnly", "Description", "GameImageId", "Name", "category", "genre" },
                values: new object[,]
                {
                    { 1, false, "It's me Mario", null, "MarioKart", 2, 10 },
                    { 2, false, "Can you buy all the houses?", null, "Monopoly", 1, 6 },
                    { 3, false, "Scribble, Scrubble, Scrabble", null, "Scrabble", 1, 7 },
                    { 4, true, "How offensive can you get?", null, "Cards against humanity", 0, 3 },
                    { 5, true, "Nice hand bro", null, "Poker", 0, 7 },
                    { 6, true, "Dont hit at 21", null, "Blackjack", 0, 7 }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "AddressId", "AlcoholFree", "DateOfBirth", "Email", "Gender", "LactoseIntolerant", "Name", "NutAllergy", "Vegan" },
                values: new object[] { 1, 1, false, new DateTime(1998, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "boris@email.com", 0, false, "Boris Pouw", true, false });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "AddressId", "AlcoholFree", "DateOfBirth", "Email", "Gender", "LactoseIntolerant", "Name", "NutAllergy", "Vegan" },
                values: new object[] { 2, 2, false, new DateTime(1999, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ntstefi@email.com", 1, false, "Stefi Nicoara", false, true });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "AddressId", "AlcoholFree", "DateOfBirth", "Email", "Gender", "LactoseIntolerant", "Name", "NutAllergy", "Vegan" },
                values: new object[] { 3, 3, true, new DateTime(2000, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "piet@email.com", 0, true, "Piet Test", true, true });

            migrationBuilder.InsertData(
                table: "GameNight",
                columns: new[] { "Id", "AddressId", "AdultsOnly", "AlcoholFree", "DateTime", "LactoseIntolerant", "MaxPlayers", "Name", "NutAllergy", "OrganiserId", "Vegan" },
                values: new object[] { 1, 1, false, false, new DateTime(2022, 10, 23, 14, 23, 34, 661, DateTimeKind.Local).AddTicks(590), false, 6, "MarioKart session", false, 1, false });

            migrationBuilder.InsertData(
                table: "GameNight",
                columns: new[] { "Id", "AddressId", "AdultsOnly", "AlcoholFree", "DateTime", "LactoseIntolerant", "MaxPlayers", "Name", "NutAllergy", "OrganiserId", "Vegan" },
                values: new object[] { 2, 3, true, false, new DateTime(2022, 10, 23, 14, 23, 34, 661, DateTimeKind.Local).AddTicks(640), false, 6, "Poker night", false, 2, false });

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
                name: "IX_Game_Name",
                table: "Game",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameImage_GameID",
                table: "GameImage",
                column: "GameID",
                unique: true,
                filter: "[GameID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_AddressId",
                table: "GameNight",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_OrganiserId",
                table: "GameNight",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_Game_GameNightId",
                table: "GameNight_Game",
                column: "GameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_Player_PersonId",
                table: "GameNight_Player",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AddressId",
                table: "Person",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Review_ReviewId",
                table: "Person_Review",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewerId",
                table: "Review",
                column: "ReviewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameImage");

            migrationBuilder.DropTable(
                name: "GameNight_Game");

            migrationBuilder.DropTable(
                name: "GameNight_Player");

            migrationBuilder.DropTable(
                name: "Person_Review");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "GameNight");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
