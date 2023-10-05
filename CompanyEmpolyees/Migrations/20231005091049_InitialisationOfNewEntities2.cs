using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmpolyees.Migrations
{
    /// <inheritdoc />
    public partial class InitialisationOfNewEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    LibraryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.LibraryId);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    ReaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    LibraryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.ReaderId);
                    table.ForeignKey(
                        name: "FK_Readers_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "LibraryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Libraries",
                columns: new[] { "LibraryId", "Address", "Name" },
                values: new object[,]
                {
                    { new Guid("83797f46-00b7-41d6-b61c-0a8a8a013cc5"), "Kakoy-to tam address", "Biblioteka 1" },
                    { new Guid("e918df18-c597-4e14-a0da-7bbfb649122c"), "Drugoy kakoy-to tam address", "Biblioteka 2" }
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "ReaderId", "Age", "LibraryId", "Name" },
                values: new object[,]
                {
                    { new Guid("50fe9ec9-094c-466b-b5c0-9a6486654289"), 31, new Guid("e918df18-c597-4e14-a0da-7bbfb649122c"), "Sasha Cherniy" },
                    { new Guid("54d10fcf-81f9-4771-b8ca-b6c090cae604"), 24, new Guid("83797f46-00b7-41d6-b61c-0a8a8a013cc5"), "Irina Belaya" },
                    { new Guid("dffcc5d0-1368-429a-95e6-76ac034cd910"), 18, new Guid("83797f46-00b7-41d6-b61c-0a8a8a013cc5"), "Lika Seraya" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Readers_LibraryId",
                table: "Readers",
                column: "LibraryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Libraries");
        }
    }
}
