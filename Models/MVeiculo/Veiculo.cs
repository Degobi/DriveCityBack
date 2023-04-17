using DriveOfCity.Models.MUsuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;

namespace DriveOfCity.Models.MVeiculo
{
    [Table("Veiculo")]
    public class Veiculo
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Marca")]
        public string Marca { get; set; }

        [Column("Modelo")]
        public string Modelo { get; set; }

        [Column("Ano")]
        [Range(1900, 2100, ErrorMessage = "O ano deve estar entre 1900 e 2100.")]
        public int Ano { get; set; }

        public Cor Cor { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    public enum Cor
    {
        Vermelho,
        Azul,
        Verde,
        Amarelo,
        Preto,
        Branco,
        Prata
    }
}
