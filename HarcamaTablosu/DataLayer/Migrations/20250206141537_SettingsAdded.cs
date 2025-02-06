using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class SettingsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ayarlar",
                columns: table => new
                {
                    AyarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BugunOncesi = table.Column<bool>(type: "bit", nullable: false),
                    KacHaftaOnce = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ayarlar", x => x.AyarId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_GiderTipleri",
                columns: table => new
                {
                    GiderTipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiderTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RenkKodu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_GiderTipleri", x => x.GiderTipId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Giderler",
                columns: table => new
                {
                    GiderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiderAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiderTipId = table.Column<int>(type: "int", nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Giderler", x => x.GiderId);
                    table.ForeignKey(
                        name: "FK_tbl_Giderler_tbl_GiderTipleri_GiderTipId",
                        column: x => x.GiderTipId,
                        principalTable: "tbl_GiderTipleri",
                        principalColumn: "GiderTipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Giderler_GiderTipId",
                table: "tbl_Giderler",
                column: "GiderTipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ayarlar");

            migrationBuilder.DropTable(
                name: "tbl_Giderler");

            migrationBuilder.DropTable(
                name: "tbl_GiderTipleri");
        }
    }
}
