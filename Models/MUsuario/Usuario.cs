using System.ComponentModel.DataAnnotations.Schema;

namespace DriveOfCity.Models.MUsuario
{
    [Table("Usuario")]
    public class Usuario
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Senha")]
        public string Senha { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }

        [Column("DataAtualizacao")]
        public DateTime DataAtualizacao { get; set; }

    }
}
