using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge24.Data.Migrations
{
    /// <inheritdoc />
    public partial class RealEstateDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileToApis",
                table: "FileToApis");

            migrationBuilder.RenameTable(
                name: "FileToApis",
                newName: "FileToApi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileToApi",
                table: "FileToApi",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileToApi",
                table: "FileToApi");

            migrationBuilder.RenameTable(
                name: "FileToApi",
                newName: "FileToApis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileToApis",
                table: "FileToApis",
                column: "Id");
        }
    }
}
