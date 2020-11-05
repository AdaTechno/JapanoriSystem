using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using System.Web.Mvc;

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

        [DisplayName("Valor Total")]
        public double PrecoTotal { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Produtos")]
        public virtual ICollection<ProdutoComanda> Produtos { get; set; }
        

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

        public string Status { get; set; }

        [DisplayName("Itens do Estoque")]
        public virtual ICollection<EstoqueProduto> EstoqueItens { get; set; }

        public virtual ICollection<ProdutoComanda> Comandas { get; set; }
        

    }

    [Table("tbProdutoComanda")]
    public class ProdutoComanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdutoComandaID { get; set; }
        [Column(Order = 1)]
        [ForeignKey("Comanda")]
        public int ComandaID { get; set; }

        [Column(Order = 2)]
        [ForeignKey("Produto")]
        public int ProdutoID { get; set; }

        public int Quantidade { get; set; }

        public virtual Comanda Comanda { get; set; }
        public virtual Produto Produto { get; set; }

        
    }

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