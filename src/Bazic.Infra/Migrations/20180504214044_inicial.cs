using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bazic.Infra.Data.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaTipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AtualizadoEm = table.Column<DateTime>(nullable: true),
                    AtualizadoPor = table.Column<string>(nullable: true),
                    CriadoEm = table.Column<DateTime>(nullable: true),
                    CriadoPor = table.Column<string>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    DeletadoEm = table.Column<DateTime>(nullable: true),
                    DeletadoPor = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaTipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AtualizadoEm = table.Column<DateTime>(nullable: true),
                    AtualizadoPor = table.Column<string>(nullable: true),
                    CriadoEm = table.Column<DateTime>(nullable: true),
                    CriadoPor = table.Column<string>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    DeletadoEm = table.Column<DateTime>(nullable: true),
                    DeletadoPor = table.Column<string>(nullable: true),
                    Id_contaTipo = table.Column<Guid>(nullable: false),
                    NomeCompleto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contas_ContaTipos_Id_contaTipo",
                        column: x => x.Id_contaTipo,
                        principalTable: "ContaTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contas_Id_contaTipo",
                table: "Contas",
                column: "Id_contaTipo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "ContaTipos");
        }
    }
}
