using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoBart.Migrations
{
    public partial class nueva_instalacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 15, nullable: false),
                    Apellido = table.Column<string>(maxLength: 15, nullable: false),
                    Username = table.Column<string>(maxLength: 15, nullable: false),
                    Password = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 15, nullable: false),
                    Apellido = table.Column<string>(maxLength: 15, nullable: false),
                    Username = table.Column<string>(maxLength: 15, nullable: false),
                    Password = table.Column<string>(maxLength: 15, nullable: false),
                    Telefono = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(maxLength: 20, nullable: false),
                    Piso = table.Column<int>(nullable: false),
                    Departamento = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Platos",
                columns: table => new
                {
                    IdPlato = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Precio = table.Column<double>(nullable: false),
                    Categoria = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platos", x => x.IdPlato);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClienteIdUsuario = table.Column<int>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    Monto = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_IdClienteIdUsuario",
                        column: x => x.IdClienteIdUsuario,
                        principalTable: "Clientes",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlatosXPedidos",
                columns: table => new
                {
                    IdPlatoXPedido = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(nullable: false),
                    IdPlato = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatosXPedidos", x => x.IdPlatoXPedido);
                    table.ForeignKey(
                        name: "FK_PlatosXPedidos_Pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatosXPedidos_Platos_IdPlato",
                        column: x => x.IdPlato,
                        principalTable: "Platos",
                        principalColumn: "IdPlato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdClienteIdUsuario",
                table: "Pedidos",
                column: "IdClienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PlatosXPedidos_IdPedido",
                table: "PlatosXPedidos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_PlatosXPedidos_IdPlato",
                table: "PlatosXPedidos",
                column: "IdPlato");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "PlatosXPedidos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Platos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
