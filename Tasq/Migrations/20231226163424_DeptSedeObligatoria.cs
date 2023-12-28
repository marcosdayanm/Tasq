using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasq.Migrations
{
    public partial class DeptSedeObligatoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_Sedes_IdSede",
                table: "Departamentos");

            migrationBuilder.AlterColumn<int>(
                name: "IdSede",
                table: "Departamentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departamentos_Sedes_IdSede",
                table: "Departamentos",
                column: "IdSede",
                principalTable: "Sedes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_Sedes_IdSede",
                table: "Departamentos");

            migrationBuilder.AlterColumn<int>(
                name: "IdSede",
                table: "Departamentos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamentos_Sedes_IdSede",
                table: "Departamentos",
                column: "IdSede",
                principalTable: "Sedes",
                principalColumn: "Id");
        }
    }
}
