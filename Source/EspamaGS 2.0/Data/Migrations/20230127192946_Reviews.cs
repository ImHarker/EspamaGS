using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EspamaGS_2._0.Data.Migrations
{
    public partial class Reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundAnim",
                table: "UserSettings");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewMessage = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IdJogo = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Jogo_IdJogo",
                        column: x => x.IdJogo,
                        principalTable: "Jogo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IdJogo",
                table: "Reviews",
                column: "IdJogo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.AddColumn<bool>(
                name: "BackgroundAnim",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
