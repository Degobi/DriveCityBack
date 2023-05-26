using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveOfCity.Migrations
{
    public partial class VeiculoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Veiculo");

            migrationBuilder.RenameColumn(
                name: "Modelo",
                table: "Veiculo",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "Marca",
                table: "Veiculo",
                newName: "Placa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Veiculo",
                newName: "Modelo");

            migrationBuilder.RenameColumn(
                name: "Placa",
                table: "Veiculo",
                newName: "Marca");

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "Veiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cor",
                table: "Veiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
