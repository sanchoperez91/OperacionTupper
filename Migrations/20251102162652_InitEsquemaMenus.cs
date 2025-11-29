using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OperacionTupper2._0.Migrations
{
    /// <inheritdoc />
    public partial class InitEsquemaMenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    IdIngrediente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreIngrediente = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.IdIngrediente);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    IdMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMenu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DiasMenu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.IdMenu);
                });

            migrationBuilder.CreateTable(
                name: "Platos",
                columns: table => new
                {
                    IdPlato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePlato = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HoraComida = table.Column<int>(type: "int", nullable: false),
                    AcompanamientoPrincipal = table.Column<int>(type: "int", nullable: false),
                    PlatoUnico = table.Column<int>(type: "int", nullable: false),
                    GrupoNutricional = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platos", x => x.IdPlato);
                });

            migrationBuilder.CreateTable(
                name: "MenusDetalles",
                columns: table => new
                {
                    IdMenuDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMenu = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraComida = table.Column<int>(type: "int", nullable: false),
                    IdPlato = table.Column<int>(type: "int", nullable: false),
                    MenuIdMenu = table.Column<int>(type: "int", nullable: false),
                    PlatoIdPlato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenusDetalles", x => x.IdMenuDetalle);
                    table.ForeignKey(
                        name: "FK_MenusDetalles_Menus_MenuIdMenu",
                        column: x => x.MenuIdMenu,
                        principalTable: "Menus",
                        principalColumn: "IdMenu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenusDetalles_Platos_PlatoIdPlato",
                        column: x => x.PlatoIdPlato,
                        principalTable: "Platos",
                        principalColumn: "IdPlato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatoIngredientes",
                columns: table => new
                {
                    IdPlatoIngrediente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPlato = table.Column<int>(type: "int", nullable: false),
                    IdIngrediente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatoIngredientes", x => x.IdPlatoIngrediente);
                    table.ForeignKey(
                        name: "FK_PlatoIngredientes_Ingredientes_IdIngrediente",
                        column: x => x.IdIngrediente,
                        principalTable: "Ingredientes",
                        principalColumn: "IdIngrediente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatoIngredientes_Platos_IdPlato",
                        column: x => x.IdPlato,
                        principalTable: "Platos",
                        principalColumn: "IdPlato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenusDetalles_IdMenu_DiaSemana_HoraComida",
                table: "MenusDetalles",
                columns: new[] { "IdMenu", "DiaSemana", "HoraComida" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenusDetalles_MenuIdMenu",
                table: "MenusDetalles",
                column: "MenuIdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_MenusDetalles_PlatoIdPlato",
                table: "MenusDetalles",
                column: "PlatoIdPlato");

            migrationBuilder.CreateIndex(
                name: "IX_PlatoIngredientes_IdIngrediente",
                table: "PlatoIngredientes",
                column: "IdIngrediente");

            migrationBuilder.CreateIndex(
                name: "IX_PlatoIngredientes_IdPlato",
                table: "PlatoIngredientes",
                column: "IdPlato");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenusDetalles");

            migrationBuilder.DropTable(
                name: "PlatoIngredientes");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Platos");
        }
    }
}
