using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EspamaGS_2._0.Data.Migrations
{
    public partial class Reviews2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataReview",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataReview",
                table: "Reviews");
        }
    }
}
