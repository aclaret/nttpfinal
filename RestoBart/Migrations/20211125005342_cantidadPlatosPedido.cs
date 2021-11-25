using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoBart.Migrations
{
    public partial class cantidadPlatosPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "PlatosXPedidos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "PlatosXPedidos");
        }
    }
}
