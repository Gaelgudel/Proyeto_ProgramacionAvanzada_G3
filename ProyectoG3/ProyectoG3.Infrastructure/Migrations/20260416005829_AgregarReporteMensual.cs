using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoG3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarReporteMensual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportesMensuales",
                columns: table => new
                {
                    IdReporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdComercio = table.Column<int>(type: "int", nullable: false),
                    CantidadDeCajas = table.Column<int>(type: "int", nullable: false),
                    MontoTotalRecaudado = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CantidadDeSINPES = table.Column<int>(type: "int", nullable: false),
                    MontoTotalComision = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FechaDelReporte = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ComercioIdComercio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportesMensuales", x => x.IdReporte);
                    table.ForeignKey(
                        name: "FK_ReportesMensuales_Comercios_ComercioIdComercio",
                        column: x => x.ComercioIdComercio,
                        principalTable: "Comercios",
                        principalColumn: "IdComercio");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesMensuales_ComercioIdComercio",
                table: "ReportesMensuales",
                column: "ComercioIdComercio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportesMensuales");
        }
    }
}