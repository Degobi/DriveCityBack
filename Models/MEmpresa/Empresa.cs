using System.ComponentModel.DataAnnotations.Schema;

namespace DriveOfCity.Models.MEmpresa
{
    [Table("Empresa")]
    public class Empresa
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }

        [Column("Lat")]
        public string Lat { get; set; }

        [Column("Lng")]
        public string Lng { get; set; }
    }
}
