using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EspamaGS_2._0.Data.Migrations
{
    public partial class DbNova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    ID_UTILIZADOR = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_ADMIN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DATA_REGISTO = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Administ__7BC3137CC20F3687", x => x.ID_UTILIZADOR);
                    table.ForeignKey(
                        name: "FK__Administr__ID_AD__1332DBDC",
                        column: x => x.ID_ADMIN,
                        principalTable: "Administrador",
                        principalColumn: "ID_UTILIZADOR");
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Desenvolvedora",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desenvolvedora", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Plataforma",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataforma", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    ID_UTILIZADOR = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TELEFONE = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    ID_ADMIN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DATA_REGISTO = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Funciona__7BC3137C400E1255", x => x.ID_UTILIZADOR);
                    table.ForeignKey(
                        name: "FK__Funcionar__ID_AD__160F4887",
                        column: x => x.ID_ADMIN,
                        principalTable: "Administrador",
                        principalColumn: "ID_UTILIZADOR");
                });

            migrationBuilder.CreateTable(
                name: "Preferencia",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_CATEGORIA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Preferen__671E10CADF476741", x => new { x.ID_CLIENTE, x.ID_CATEGORIA });
                    table.ForeignKey(
                        name: "FK__Preferenc__ID_CA__1EA48E88",
                        column: x => x.ID_CATEGORIA,
                        principalTable: "Categoria",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FOTO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: false),
                    PRECO = table.Column<decimal>(type: "money", nullable: false),
                    ID_CATEGORIA = table.Column<int>(type: "int", nullable: false),
                    ID_DESENVOLVEDORA = table.Column<int>(type: "int", nullable: false),
                    ID_PLATAFORMA = table.Column<int>(type: "int", nullable: false),
                    ID_FUNCIONARIO = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DATA_REGISTO = table.Column<DateTime>(type: "datetime", nullable: false),
                    DATA_LANCAMENTO = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Jogo__ID_CATEGOR__18EBB532",
                        column: x => x.ID_CATEGORIA,
                        principalTable: "Categoria",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Jogo__ID_DESENVO__19DFD96B",
                        column: x => x.ID_DESENVOLVEDORA,
                        principalTable: "Desenvolvedora",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Jogo__ID_FUNCION__1BC821DD",
                        column: x => x.ID_FUNCIONARIO,
                        principalTable: "Funcionario",
                        principalColumn: "ID_UTILIZADOR");
                    table.ForeignKey(
                        name: "FK__Jogo__ID_PLATAFO__1AD3FDA4",
                        column: x => x.ID_PLATAFORMA,
                        principalTable: "Plataforma",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdJogo = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Cart__IdJogo__160F4887",
                        column: x => x.IdJogo,
                        principalTable: "Jogo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_JOGO = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    KEY_JOGO = table.Column<string>(type: "varchar(18)", unicode: false, maxLength: 18, nullable: false),
                    DATA_COMPRA = table.Column<DateTime>(type: "datetime", nullable: false),
                    PRECO = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Compra__6B235F1E45A1618F", x => new { x.ID });
                    table.ForeignKey(
                        name: "FK__Compra__ID_JOGO__2180FB33",
                        column: x => x.ID_JOGO,
                        principalTable: "Jogo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_ID_ADMIN",
                table: "Administrador",
                column: "ID_ADMIN");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_IdJogo",
                table: "Cart",
                column: "IdJogo");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_ID_JOGO",
                table: "Compra",
                column: "ID_JOGO");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_ID_ADMIN",
                table: "Funcionario",
                column: "ID_ADMIN");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_ID_CATEGORIA",
                table: "Jogo",
                column: "ID_CATEGORIA");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_ID_DESENVOLVEDORA",
                table: "Jogo",
                column: "ID_DESENVOLVEDORA");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_ID_FUNCIONARIO",
                table: "Jogo",
                column: "ID_FUNCIONARIO");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_ID_PLATAFORMA",
                table: "Jogo",
                column: "ID_PLATAFORMA");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencia_ID_CATEGORIA",
                table: "Preferencia",
                column: "ID_CATEGORIA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Preferencia");

            migrationBuilder.DropTable(
                name: "Jogo");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Desenvolvedora");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Plataforma");

            migrationBuilder.DropTable(
                name: "Administrador");
        }
    }
}
