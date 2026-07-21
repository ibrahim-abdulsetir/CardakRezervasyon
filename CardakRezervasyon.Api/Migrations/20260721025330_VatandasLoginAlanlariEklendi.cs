using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardakRezervasyon.Api.Migrations
{
    /// <inheritdoc />
    public partial class VatandasLoginAlanlariEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdSoyad",
                table: "Vatandaslar",
                newName: "Soyad");

            migrationBuilder.AlterColumn<string>(
                name: "Eposta",
                table: "Vatandaslar",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Vatandaslar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "AktifMi",
                table: "Vatandaslar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParolaHash",
                table: "Vatandaslar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vatandaslar_Eposta",
                table: "Vatandaslar",
                column: "Eposta",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vatandaslar_Eposta",
                table: "Vatandaslar");

            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Vatandaslar");

            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "Vatandaslar");

            migrationBuilder.DropColumn(
                name: "ParolaHash",
                table: "Vatandaslar");

            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "Vatandaslar",
                newName: "AdSoyad");

            migrationBuilder.AlterColumn<string>(
                name: "Eposta",
                table: "Vatandaslar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
