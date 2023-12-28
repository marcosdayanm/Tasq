using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasq.Migrations
{
    public partial class ArregleFKDepartamentoUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdDepartamento",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdDepartamento",
                table: "AspNetUsers",
                column: "IdDepartamento");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departamentos_IdDepartamento",
                table: "AspNetUsers",
                column: "IdDepartamento",
                principalTable: "Departamentos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departamentos_IdDepartamento",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdDepartamento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdDepartamento",
                table: "AspNetUsers");
        }
    }
}
