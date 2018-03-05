using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProyectoWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CedulaJuridica = table.Column<int>(nullable: false),
                    DireccionFisica = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    NúmerodeTelefono = table.Column<int>(nullable: false),
                    PaginaWeb = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Apellidos = table.Column<string>(nullable: true),
                    NicName = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    TipoUsuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "contacto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Apellidos = table.Column<string>(nullable: true),
                    ClienteID = table.Column<int>(nullable: false),
                    Correo = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Telefono = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_contacto_cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "cliente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "soporte",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteID = table.Column<int>(nullable: false),
                    DetalleDelProblema = table.Column<string>(nullable: true),
                    EstadoActual = table.Column<string>(nullable: true),
                    QuienReporto = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_soporte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_soporte_cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "cliente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reunion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    TituloDeLaReunion = table.Column<string>(nullable: true),
                    UsuarioAsignado = table.Column<string>(nullable: true),
                    UsuarioID = table.Column<int>(nullable: false),
                    Virtual = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reunion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_reunion_usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contacto_ClienteID",
                table: "contacto",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_reunion_UsuarioID",
                table: "reunion",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_soporte_ClienteID",
                table: "soporte",
                column: "ClienteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacto");

            migrationBuilder.DropTable(
                name: "reunion");

            migrationBuilder.DropTable(
                name: "soporte");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "cliente");
        }
    }
}
