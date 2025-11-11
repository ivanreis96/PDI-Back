using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProject.App.Migrations
{
    /// <inheritdoc />
    public partial class CriarModeloReceitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Apelido = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    Whatsapp = table.Column<string>(type: "TEXT", nullable: true),
                    Instagram = table.Column<string>(type: "TEXT", nullable: true),
                    TikTok = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    ModoPreparo = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receitas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicacoesReceitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReceitaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NotaMedia = table.Column<decimal>(type: "TEXT", precision: 3, scale: 2, nullable: false),
                    QuantidadeVotos = table.Column<int>(type: "INTEGER", nullable: false),
                    Deliciosos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicacoesReceitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicacoesReceitas_Receitas_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaIngredientes",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaIngredientes", x => new { x.ReceitaId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_ReceitaIngredientes_Ingredientes_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitaIngredientes_Receitas_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitasAmadas",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceitaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataMarcacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitasAmadas", x => new { x.UsuarioId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ReceitasAmadas_Receitas_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitasAmadas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitasDeliciosas",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceitaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataMarcacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitasDeliciosas", x => new { x.UsuarioId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ReceitasDeliciosas_Receitas_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitasDeliciosas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_Nome",
                table: "Ingredientes",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacoesReceitas_DataPublicacao",
                table: "PublicacoesReceitas",
                column: "DataPublicacao");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacoesReceitas_ReceitaId",
                table: "PublicacoesReceitas",
                column: "ReceitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaIngredientes_IngredienteId",
                table: "ReceitaIngredientes",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_UsuarioId",
                table: "Receitas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitasAmadas_ReceitaId",
                table: "ReceitasAmadas",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitasDeliciosas_ReceitaId",
                table: "ReceitasDeliciosas",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Apelido",
                table: "Usuarios",
                column: "Apelido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicacoesReceitas");

            migrationBuilder.DropTable(
                name: "ReceitaIngredientes");

            migrationBuilder.DropTable(
                name: "ReceitasAmadas");

            migrationBuilder.DropTable(
                name: "ReceitasDeliciosas");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Receitas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
