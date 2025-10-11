using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge24.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddKindergartenIdToFileToApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KindergartenId",
                table: "FileToApis",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KindergartenId",
                table: "FileToApis");
        }
    }
}
