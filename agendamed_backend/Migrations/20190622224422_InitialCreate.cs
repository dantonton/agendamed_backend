using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace agendamed_backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compromisso",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Nascimento = table.Column<DateTimeOffset>(nullable: false),
                    Inicio = table.Column<DateTimeOffset>(nullable: false),
                    Fim = table.Column<DateTimeOffset>(nullable: false),
                    Observacao = table.Column<string>(maxLength: 5000, nullable: true),
                    Registro = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compromisso", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compromisso_Inicio_Fim",
                table: "Compromisso",
                columns: new[] { "Inicio", "Fim" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compromisso");
        }
    }
}
