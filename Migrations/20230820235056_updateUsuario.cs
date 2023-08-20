using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveOfCity.Migrations
{
    public partial class updateUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabela_Preco_Empresa_EmpresaId",
                table: "Tabela_Preco");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Usuario_UsuarioId",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_UsuarioId",
                table: "Veiculo");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Tabela_Preco",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tabela_Preco_Empresa_EmpresaId",
                table: "Tabela_Preco",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabela_Preco_Empresa_EmpresaId",
                table: "Tabela_Preco");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Tabela_Preco",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_UsuarioId",
                table: "Veiculo",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabela_Preco_Empresa_EmpresaId",
                table: "Tabela_Preco",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Usuario_UsuarioId",
                table: "Veiculo",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
