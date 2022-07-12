using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.Migrations
{
    public partial class Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "938cac7c-6ce2-4e86-ba6e-be4c2705b515");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b37f2312-fa6e-4708-a98a-3926993c3425");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3815d9a4-a8ac-4aa6-a278-441ce44a2511", "1fc22f8a-dbc8-47d4-8e39-8099a75aefa7", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7704ee39-a659-4a59-b3d7-ddd0749ca392", "e763ce94-b1db-46ce-b0e3-f61f1969d955", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "92dc5a1f-5539-4aae-9cdf-2378762698aa", "d3feb500-46e2-4e16-bc7b-f60b7a515dd4", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3815d9a4-a8ac-4aa6-a278-441ce44a2511");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7704ee39-a659-4a59-b3d7-ddd0749ca392");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92dc5a1f-5539-4aae-9cdf-2378762698aa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "938cac7c-6ce2-4e86-ba6e-be4c2705b515", "c8a623ef-fda4-4fc6-8589-1e9493ebe6dd", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b37f2312-fa6e-4708-a98a-3926993c3425", "b6215e29-8a1d-47ac-b3b5-d4632a3ae74c", "Administrator", "Administrator" });
        }
    }
}
