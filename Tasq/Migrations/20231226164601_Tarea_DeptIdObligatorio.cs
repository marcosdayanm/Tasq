using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasq.Migrations
{
    public partial class Tarea_DeptIdObligatorio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Departamentos_IdDepartamento",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "IdDepartamento",
                table: "Tareas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Departamentos_IdDepartamento",
                table: "Tareas",
                column: "IdDepartamento",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Departamentos_IdDepartamento",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "IdDepartamento",
                table: "Tareas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Departamentos_IdDepartamento",
                table: "Tareas",
                column: "IdDepartamento",
                principalTable: "Departamentos",
                principalColumn: "Id");
        }
    }
}
