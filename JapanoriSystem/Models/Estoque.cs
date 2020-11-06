using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JapanoriSystem.Models
{
    [Table("tbEstoque")]
    public class Estoque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public string TipoQuantidade { get; set; }
        public string Categoria { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Último carregamento")]
        public DateTime UltimoCarregamento { get; set; }
        public string Obs { get; set; }
        public string Status { get; set; }

        public virtual ICollection<EstoqueProduto> Produtos { get; set; }
    }

    [Table("tbEstoqueProduto")]
    public class EstoqueProduto
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Produto")]
        public int ProdutoID { get; set; }
        [Key, Column(Order = 2)]
        [ForeignKey("Estoque")]
        public int ItemID { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}