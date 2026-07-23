using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardakRezervasyon.Api.Migrations
{
    /// <inheritdoc />
    public partial class GirisKilitlemeEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasarisizGirisSayisi",
                table: "Vatandaslar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "KilitlenmeZamani",
                table: "Vatandaslar",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasarisizGirisSayisi",
                table: "Vatandaslar");

            migrationBuilder.DropColumn(
                name: "KilitlenmeZamani",
                table: "Vatandaslar");
        }
    }
}
