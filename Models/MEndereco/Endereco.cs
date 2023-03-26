using System.ComponentModel.DataAnnotations.Schema;

namespace DriveOfCity.Models.MEndereco
{
    [Table("Endereco")]
    public class Endereco
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Numero")]
        public string Numero { get; set; }

        [Column("Rua")]
        public string Rua { get; set; }

        [Column("Cep")]
        public string Cep { get; set; }

        [Column("Cidade")]
        public string Cidade { get; set; }

        [Column("Bairro")]
        public string Bairro { get; set; }

        [Column("Estado")]
        public string Estado { get; set; }

        [Column("Referencia")]
        public string Referencia { get; set;}
    }
}
