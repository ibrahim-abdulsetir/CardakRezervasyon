using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardakRezervasyon.Api.Migrations
{
    /// <inheritdoc />
    public partial class VatandasTokenEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Vatandaslar",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Vatandaslar");
        }
    }
}
