using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;

namespace JapanoriSystem.Models
{
    // Relacionamento Comanda - Produto - Estoque

    
    [Table("tbComanda")]
    public class Comanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None), DisplayName("Código da Comanda")]
        public int ID { get; set; }

        [DisplayName("Situação")]
        public string Situacao { get; set; }

        [DisplayName("Quantidade de Produtos")]
        public virtual ICollection<ComandaProduto> Produtos { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Valor Total")]
        public double PrecoTotal { get; set; }
    }

    [Table("tbProduto")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public string Desc { get; set; }
        [DisplayName("Valor Unitário")]
        public double Preco { get; set; }

        [DisplayName("Itens do Estoque")]
        public virtual ICollection<ProdutoEstoque> EstoqueItens { get; set; }
        public string Status { get; set; }

    }

    [Table("tbComandaProduto")]
    public class ComandaProduto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComandaProdutoID { get; set; }

        public int ComandaID { get; set; }

        public int ProdutoID { get; set; }

        public virtual Comanda Comanda { get; set; }
        public virtual Produto Produto { get; set; }
    }

    public enum TipoQuantidade
    {
        Unidades, Quilos, Gramas, Miligramas, Litros, Mililitros, Centímetros, Metros
    }

    [Table("tbEstoque")]
    public class Estoque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstoqueID { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public TipoQuantidade TipoQuantidade { get; set; }
        public DateTime UltimoCarregamento { get; set; }
        public string Status { get; set; }
    }

    [Table("tbProdutoEstoque")]
    public class ProdutoEstoque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdutoEstoqueID { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoID { get; set; }

        [ForeignKey("Estoque")]
        public int EstoqueID { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Estoque Estoque { get; set; }
    }

}