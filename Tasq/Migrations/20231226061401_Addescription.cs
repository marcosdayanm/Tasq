using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasq.Migrations
{
    public partial class Addescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Departamentos",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Departamentos");
        }
    }
}
