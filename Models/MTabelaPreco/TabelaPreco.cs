using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveOfCity.Models.MTabelaPreco
{
    [Table("Tabela_Preco")]
    public class TabelaPreco
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Descricao_Servico")]
        public string DescricaoServico { get; set; }

        [Column("Valor_Servico")]
        public decimal ValorServico { get; set; }

        [Column("Promocao")]
        [DefaultValue(false)]
        public bool Promocao { get; set; }

        [Column("Desconto_Promocao")]
        public decimal DescontoPromocao { get; set; }

        [Column("EmpresaId")]
        public int EmpresaId { get; set; }
    }
}
