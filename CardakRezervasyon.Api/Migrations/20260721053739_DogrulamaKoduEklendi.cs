using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardakRezervasyon.Api.Migrations
{
    /// <inheritdoc />
    public partial class DogrulamaKoduEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DogrulamaKodlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VatandasId = table.Column<int>(type: "int", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SonGecerlilikZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KullanildiMi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogrulamaKodlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DogrulamaKodlari_Vatandaslar_VatandasId",
                        column: x => x.VatandasId,
                        principalTable: "Vatandaslar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DogrulamaKodlari_VatandasId",
                table: "DogrulamaKodlari",
                column: "VatandasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DogrulamaKodlari");
        }
    }
}
