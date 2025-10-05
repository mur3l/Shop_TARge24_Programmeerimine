using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge24.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRealEstateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "RealEstates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomNumber = table.Column<int>(type: "int", nullable: true),
                    BuildingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstates");

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
    }
}
