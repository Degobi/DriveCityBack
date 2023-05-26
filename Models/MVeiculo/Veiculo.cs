using DriveOfCity.Models.MUsuario;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveOfCity.Models.MVeiculo
{
    [Table("Veiculo")]
    public class Veiculo
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("Placa")]
        public string Placa { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [NotMapped]
        public virtual Usuario Usuario { get; set; }
    }

}
