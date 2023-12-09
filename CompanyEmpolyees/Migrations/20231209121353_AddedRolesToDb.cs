using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmpolyees.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b59b1c8d-5b21-4846-8282-f6ddd6fb2533", "964d0a2a-1531-4d6a-a357-5cc18a4b0b12", "Manager", "MANAGER" },
                    { "ecaee857-6445-48a2-b2fb-0c66309cdfa5", "2a5c688c-1517-4d29-9eab-1fb2d196630a", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b59b1c8d-5b21-4846-8282-f6ddd6fb2533");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecaee857-6445-48a2-b2fb-0c66309cdfa5");
        }
    }
}
