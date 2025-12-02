using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge24.Data.Migrations
{
    /// <inheritdoc />
    public partial class SyncModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Area",
                table: "FileToApis",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingType",
                table: "FileToApis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAd",
                table: "FileToApis",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "FileToApis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAd",
                table: "FileToApis",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "FileToApis",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "FileToApis");

            migrationBuilder.DropColumn(
                name: "BuildingType",
                table: "FileToApis");

            migrationBuilder.DropColumn(
                name: "CreatedAd",
                table: "FileToApis");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "FileToApis");

            migrationBuilder.DropColumn(
                name: "UpdatedAd",
                table: "FileToApis");

            migrationBuilder.DropColumn(
                name: "location",
                table: "FileToApis");
        }
    }
}
