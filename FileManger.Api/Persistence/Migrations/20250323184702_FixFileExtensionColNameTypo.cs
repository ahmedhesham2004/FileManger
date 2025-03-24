using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileManger.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixFileExtensionColNameTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileExtenstion",
                table: "Files",
                newName: "FileExtension");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileExtension",
                table: "Files",
                newName: "FileExtenstion");
        }
    }
}
