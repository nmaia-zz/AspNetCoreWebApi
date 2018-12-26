using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Climate = table.Column<string>(type: "varchar(100)", nullable: false),
                    Terrain = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Planets_Name",
                table: "Planets",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planets");
        }
    }
}
