using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "938cac7c-6ce2-4e86-ba6e-be4c2705b515", "c8a623ef-fda4-4fc6-8589-1e9493ebe6dd", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b37f2312-fa6e-4708-a98a-3926993c3425", "b6215e29-8a1d-47ac-b3b5-d4632a3ae74c", "Administrator", "Administrator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "938cac7c-6ce2-4e86-ba6e-be4c2705b515");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b37f2312-fa6e-4708-a98a-3926993c3425");
        }
    }
}
