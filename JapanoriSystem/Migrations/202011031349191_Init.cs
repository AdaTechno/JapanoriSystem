namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                    })
                .PrimaryKey(t => new { t.ComandaID, t.ProdutoID })
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
            DropForeignKey("dbo.tbProdutoComanda", "ProdutoID", "dbo.tbProduto");
            DropForeignKey("dbo.tbEstoqueProduto", "ProdutoID", "dbo.tbProduto");
            DropForeignKey("dbo.tbEstoqueProduto", "ItemID", "dbo.tbEstoque");
            DropForeignKey("dbo.tbProdutoComanda", "ComandaID", "dbo.tbComanda");
            DropIndex("dbo.tbEstoqueProduto", new[] { "ItemID" });
            DropIndex("dbo.tbEstoqueProduto", new[] { "ProdutoID" });
            DropIndex("dbo.tbProdutoComanda", new[] { "ProdutoID" });
            DropIndex("dbo.tbProdutoComanda", new[] { "ComandaID" });
            DropTable("dbo.tbFuncionario");
            DropTable("dbo.tbEstoque");
            DropTable("dbo.tbEstoqueProduto");
            DropTable("dbo.tbProduto");
            DropTable("dbo.tbProdutoComanda");
            DropTable("dbo.tbComanda");
        }
    }
}
