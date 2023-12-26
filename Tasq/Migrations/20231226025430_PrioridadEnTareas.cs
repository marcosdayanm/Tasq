using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasq.Migrations
{
    public partial class PrioridadEnTareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prioridad",
                table: "Tareas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prioridad",
                table: "Tareas");
        }
    }
}
