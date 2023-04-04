using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveOfCity.Migrations
{
    public partial class TabelaPreco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tabela_Preco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao_Servico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor_Servico = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Promocao = table.Column<bool>(type: "bit", nullable: false),
                    Desconto_Promocao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabela_Preco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tabela_Preco_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tabela_Preco_EmpresaId",
                table: "Tabela_Preco",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tabela_Preco");
        }
    }
}
