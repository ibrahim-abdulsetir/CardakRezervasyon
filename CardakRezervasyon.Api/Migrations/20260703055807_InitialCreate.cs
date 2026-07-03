using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardakRezervasyon.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MesireAlanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcilisSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    KapanisSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesireAlanlari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vatandaslar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vatandaslar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakimKapaliGunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesireAlaniId = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sebep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakimKapaliGunler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BakimKapaliGunler_MesireAlanlari_MesireAlaniId",
                        column: x => x.MesireAlaniId,
                        principalTable: "MesireAlanlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cardaklar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesireAlaniId = table.Column<int>(type: "int", nullable: false),
                    Numara = table.Column<int>(type: "int", nullable: false),
                    Blok = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kapasite = table.Column<int>(type: "int", nullable: false),
                    MangalliMi = table.Column<bool>(type: "bit", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cardaklar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cardaklar_MesireAlanlari_MesireAlaniId",
                        column: x => x.MesireAlaniId,
                        principalTable: "MesireAlanlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervasyonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardakId = table.Column<int>(type: "int", nullable: false),
                    VatandasId = table.Column<int>(type: "int", nullable: false),
                    BaslangicZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KisiSayisi = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    Not = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervasyonlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Cardaklar_CardakId",
                        column: x => x.CardakId,
                        principalTable: "Cardaklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Vatandaslar_VatandasId",
                        column: x => x.VatandasId,
                        principalTable: "Vatandaslar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BakimKapaliGunler_MesireAlaniId",
                table: "BakimKapaliGunler",
                column: "MesireAlaniId");

            migrationBuilder.CreateIndex(
                name: "IX_Cardaklar_MesireAlaniId_AktifMi",
                table: "Cardaklar",
                columns: new[] { "MesireAlaniId", "AktifMi" });

            migrationBuilder.CreateIndex(
                name: "IX_Cardaklar_MesireAlaniId_Numara",
                table: "Cardaklar",
                columns: new[] { "MesireAlaniId", "Numara" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_CardakId_BaslangicZamani_BitisZamani",
                table: "Rezervasyonlar",
                columns: new[] { "CardakId", "BaslangicZamani", "BitisZamani" });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_VatandasId",
                table: "Rezervasyonlar",
                column: "VatandasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BakimKapaliGunler");

            migrationBuilder.DropTable(
                name: "Rezervasyonlar");

            migrationBuilder.DropTable(
                name: "Cardaklar");

            migrationBuilder.DropTable(
                name: "Vatandaslar");

            migrationBuilder.DropTable(
                name: "MesireAlanlari");
        }
    }
}
