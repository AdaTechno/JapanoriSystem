namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
            
            CreateTable(
                "dbo.ProdutoComanda",
                c => new
                    {
                        Produto_ProdutoID = c.Int(nullable: false),
                        Comanda_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Produto_ProdutoID, t.Comanda_ID })
                .ForeignKey("dbo.tbProduto", t => t.Produto_ProdutoID, cascadeDelete: true)
                .ForeignKey("dbo.tbComanda", t => t.Comanda_ID, cascadeDelete: true)
                .Index(t => t.Produto_ProdutoID)
                .Index(t => t.Comanda_ID);
            
            CreateTable(
                "dbo.EstoqueProduto",
                c => new
                    {
                        Estoque_ItemID = c.Int(nullable: false),
                        Produto_ProdutoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Estoque_ItemID, t.Produto_ProdutoID })
                .ForeignKey("dbo.tbEstoque", t => t.Estoque_ItemID, cascadeDelete: true)
                .ForeignKey("dbo.tbProduto", t => t.Produto_ProdutoID, cascadeDelete: true)
                .Index(t => t.Estoque_ItemID)
                .Index(t => t.Produto_ProdutoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EstoqueProduto", "Produto_ProdutoID", "dbo.tbProduto");
            DropForeignKey("dbo.EstoqueProduto", "Estoque_ItemID", "dbo.tbEstoque");
            DropForeignKey("dbo.ProdutoComanda", "Comanda_ID", "dbo.tbComanda");
            DropForeignKey("dbo.ProdutoComanda", "Produto_ProdutoID", "dbo.tbProduto");
            DropIndex("dbo.EstoqueProduto", new[] { "Produto_ProdutoID" });
            DropIndex("dbo.EstoqueProduto", new[] { "Estoque_ItemID" });
            DropIndex("dbo.ProdutoComanda", new[] { "Comanda_ID" });
            DropIndex("dbo.ProdutoComanda", new[] { "Produto_ProdutoID" });
            DropTable("dbo.EstoqueProduto");
            DropTable("dbo.ProdutoComanda");
            DropTable("dbo.tbFuncionario");
            DropTable("dbo.tbEstoque");
            DropTable("dbo.tbProduto");
            DropTable("dbo.tbComanda");
        }
    }
}
