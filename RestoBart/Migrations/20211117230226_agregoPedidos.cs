using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoBart.Migrations
{
    public partial class agregoPedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidoIdPedido",
                table: "Platos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(nullable: false),
                    Fecha = table.Column<string>(nullable: true),
                    Monto = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedido);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Platos_PedidoIdPedido",
                table: "Platos",
                column: "PedidoIdPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Platos_Pedidos_PedidoIdPedido",
                table: "Platos",
                column: "PedidoIdPedido",
                principalTable: "Pedidos",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platos_Pedidos_PedidoIdPedido",
                table: "Platos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Platos_PedidoIdPedido",
                table: "Platos");

            migrationBuilder.DropColumn(
                name: "PedidoIdPedido",
                table: "Platos");
        }
    }
}
