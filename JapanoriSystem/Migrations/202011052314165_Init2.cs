namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbComanda",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Situacao = c.String(),
                        PrecoTotal = c.Double(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tbProdutoComanda",
                c => new
                    {
                        ComandaID = c.Int(nullable: false),
                        ProdutoID = c.Int(nullable: false),
                        ProdutoComandaID = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ProdutoComandaID)
                .ForeignKey("dbo.tbComanda", t => t.ComandaID, cascadeDelete: true)
                .ForeignKey("dbo.tbProduto", t => t.ProdutoID, cascadeDelete: true)
                .Index(t => t.ComandaID)
                .Index(t => t.ProdutoID);
            
            CreateTable(
                "dbo.tbProduto",
                c => new
                    {
                        ProdutoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Desc = c.String(),
                        Preco = c.Double(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ProdutoID);
            
            CreateTable(
                "dbo.tbEstoqueProduto",
                c => new
                    {
                        ProdutoID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoID, t.ItemID })
                .ForeignKey("dbo.tbEstoque", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.tbProduto", t => t.ProdutoID, cascadeDelete: true)
                .Index(t => t.ProdutoID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.tbEstoque",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Quantidade = c.Int(nullable: false),
                        TipoQuantidade = c.String(),
                        Categoria = c.String(),
                        UltimoCarregamento = c.DateTime(nullable: false),
                        Obs = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.tbProdutoComandaVendas",
                c => new
                    {
                        VendaID = c.Int(nullable: false),
                        ProdutoComandaID = c.Int(nullable: false),
                        ProdutoComandaVendasID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ProdutoComandaVendasID)
                .ForeignKey("dbo.tbProdutoComanda", t => t.ProdutoComandaID, cascadeDelete: true)
                .ForeignKey("dbo.tbVendas", t => t.VendaID, cascadeDelete: true)
                .Index(t => t.VendaID)
                .Index(t => t.ProdutoComandaID);
            
            CreateTable(
                "dbo.tbVendas",
                c => new
                    {
                        VendaID = c.Int(nullable: false, identity: true),
                        NomeFuncionario = c.String(),
                    })
                .PrimaryKey(t => t.VendaID);
            
            CreateTable(
                "dbo.tbFuncionario",
                c => new
                    {
                        FuncionarioID = c.Int(nullable: false),
                        Nome = c.String(maxLength: 50),
                        Sobrenome = c.String(maxLength: 100),
                        DataNasc = c.DateTime(nullable: false),
                        Cargo = c.String(maxLength: 50),
                        CPF = c.String(maxLength: 14),
                        Endereco = c.String(),
                        NumeroEnd = c.String(),
                        Cep = c.String(maxLength: 10),
                        DataContratacao = c.DateTime(nullable: false),
                        EmailCorp = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        Perm = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FuncionarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbProdutoComandaVendas", "VendaID", "dbo.tbVendas");
            DropForeignKey("dbo.tbProdutoComandaVendas", "ProdutoComandaID", "dbo.tbProdutoComanda");
            DropForeignKey("dbo.tbProdutoComanda", "ProdutoID", "dbo.tbProduto");
            DropForeignKey("dbo.tbEstoqueProduto", "ProdutoID", "dbo.tbProduto");
            DropForeignKey("dbo.tbEstoqueProduto", "ItemID", "dbo.tbEstoque");
            DropForeignKey("dbo.tbProdutoComanda", "ComandaID", "dbo.tbComanda");
            DropIndex("dbo.tbProdutoComandaVendas", new[] { "ProdutoComandaID" });
            DropIndex("dbo.tbProdutoComandaVendas", new[] { "VendaID" });
            DropIndex("dbo.tbEstoqueProduto", new[] { "ItemID" });
            DropIndex("dbo.tbEstoqueProduto", new[] { "ProdutoID" });
            DropIndex("dbo.tbProdutoComanda", new[] { "ProdutoID" });
            DropIndex("dbo.tbProdutoComanda", new[] { "ComandaID" });
            DropTable("dbo.tbFuncionario");
            DropTable("dbo.tbVendas");
            DropTable("dbo.tbProdutoComandaVendas");
            DropTable("dbo.tbEstoque");
            DropTable("dbo.tbEstoqueProduto");
            DropTable("dbo.tbProduto");
            DropTable("dbo.tbProdutoComanda");
            DropTable("dbo.tbComanda");
        }
    }
}
