namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbComanda",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Situacao = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tbComandaProduto",
                c => new
                    {
                        ComandaProdutoID = c.Int(nullable: false, identity: true),
                        ComandaID = c.Int(nullable: false),
                        ProdutoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComandaProdutoID)
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
                "dbo.tbProdutoEstoque",
                c => new
                    {
                        ProdutoEstoqueID = c.Int(nullable: false, identity: true),
                        ProdutoID = c.Int(nullable: false),
                        EstoqueID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoEstoqueID)
                .ForeignKey("dbo.tbEstoque", t => t.EstoqueID, cascadeDelete: true)
                .ForeignKey("dbo.tbProduto", t => t.ProdutoID, cascadeDelete: true)
                .Index(t => t.ProdutoID)
                .Index(t => t.EstoqueID);
            
            CreateTable(
                "dbo.tbEstoque",
                c => new
                    {
                        EstoqueID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Quantidade = c.Int(nullable: false),
                        TipoQuantidade = c.Int(nullable: false),
                        UltimoCarregamento = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.EstoqueID);
            
            CreateTable(
                "dbo.tbFuncionario",
                c => new
                    {
                        FuncionarioID = c.Int(nullable: false),
                        Nome = c.String(),
                        Sobrenome = c.String(),
                        Cargo = c.String(),
                        CPF = c.String(),
                        Endereco = c.String(),
                        Cep = c.String(),
                        EmailCorp = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        Perm = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FuncionarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbComandaProduto", "ProdutoID", "dbo.tbProduto");
            DropForeignKey("dbo.tbProdutoEstoque", "ProdutoID", "dbo.tbProduto");
            DropForeignKey("dbo.tbProdutoEstoque", "EstoqueID", "dbo.tbEstoque");
            DropForeignKey("dbo.tbComandaProduto", "ComandaID", "dbo.tbComanda");
            DropIndex("dbo.tbProdutoEstoque", new[] { "EstoqueID" });
            DropIndex("dbo.tbProdutoEstoque", new[] { "ProdutoID" });
            DropIndex("dbo.tbComandaProduto", new[] { "ProdutoID" });
            DropIndex("dbo.tbComandaProduto", new[] { "ComandaID" });
            DropTable("dbo.tbFuncionario");
            DropTable("dbo.tbEstoque");
            DropTable("dbo.tbProdutoEstoque");
            DropTable("dbo.tbProduto");
            DropTable("dbo.tbComandaProduto");
            DropTable("dbo.tbComanda");
        }
    }
}
