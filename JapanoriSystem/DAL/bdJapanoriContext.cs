using JapanoriSystem.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;


namespace JapanoriSystem.DAL
{
    public class bdJapanoriContext : DbContext
    {

        public bdJapanoriContext() : base()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<bdJapanoriContext>());
        }

        public DbSet<Comanda> tbComanda { get; set; }
        public DbSet<Produto> tbProduto { get; set; }
        public DbSet<Estoque> tbEstoque { get; set; }
        public DbSet<Funcionario> tbFuncionario { get; set; }
        public DbSet<ProdutoComanda> tbProdutoComanda { get; set; }
        public DbSet<EstoqueProduto> tbEstoqueProduto { get; set; }
        public DbSet<Vendas> tbVendas { get; set; }
        public DbSet<ProdutoComandaVendas> tbProdutoComandaVendas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

        public IEnumerable<Comanda> GetComandaList()
        {
            var list = tbComanda.ToList();
            return list;
        }
        public IEnumerable<Produto> GetProdutoList()
        {
            var list = tbProduto.ToList();
            return list;
        }

        
    }
}