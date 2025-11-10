using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CI = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FotoCasa1Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoCasa2Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoCasa3Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsApi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoLog = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MetodoHttp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UrlEndpoint = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DireccionIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsApi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UrlArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    TamanoBytes = table.Column<long>(type: "bigint", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivosCliente_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosCliente_ClienteId",
                table: "ArchivosCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CI",
                table: "Clientes",
                column: "CI",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivosCliente");

            migrationBuilder.DropTable(
                name: "LogsApi");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
