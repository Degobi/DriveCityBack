
using DriveOfCity.Models.MEndereco;
using DriveOfCity.Models.MUsuario;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DriveOfCity.Models.MPerfil
{
    [Table("Perfil")]
    public class Perfil
    {
        [Column("Id")]
        public int Id { get; set; }
     
        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Sobrenome")]
        public string Sobrenome { get; set; }

        [Column("DataNascimento")]
        public DateTime DataNascimento { get; set; }

        //[Column("Imagem")]
        //[AllowNull]
        //public byte[] Imagem { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual List<Endereco> Endereco { get; set; }
    }
}
