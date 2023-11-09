using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LuisJose_AP1_P2.Server.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    EntradaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    CantidadProducida = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.EntradaID);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripción = table.Column<string>(type: "TEXT", nullable: false),
                    PrecioCompra = table.Column<double>(type: "REAL", nullable: false),
                    PrecioVenta = table.Column<double>(type: "REAL", nullable: false),
                    Existencia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoID);
                });

            migrationBuilder.CreateTable(
                name: "EntradasDetalle",
                columns: table => new
                {
                    DetalleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntradaID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoID = table.Column<int>(type: "INTEGER", nullable: false),
                    CantidadUtilizada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradasDetalle", x => x.DetalleID);
                    table.ForeignKey(
                        name: "FK_EntradasDetalle_Entradas_EntradaID",
                        column: x => x.EntradaID,
                        principalTable: "Entradas",
                        principalColumn: "EntradaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoID", "Descripción", "Existencia", "PrecioCompra", "PrecioVenta" },
                values: new object[,]
                {
                    { 1, "Maní", 250, 8.0, 15.0 },
                    { 2, "Pistachos", 300, 15.0, 30.0 },
                    { 3, "Pasas", 130, 10.0, 25.0 },
                    { 4, "Ciruelas", 350, 25.0, 50.0 },
                    { 5, "Mixto MPP", 320, 30.0, 60.0 },
                    { 6, "Mixto MPC", 310, 30.0, 60.0 },
                    { 7, "Mixto MPP", 250, 25.0, 50.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntradasDetalle_EntradaID",
                table: "EntradasDetalle",
                column: "EntradaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntradasDetalle");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Entradas");
        }
    }
}
