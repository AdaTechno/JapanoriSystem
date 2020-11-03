namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProdutoComanda : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tbProdutoComanda");
            AddColumn("dbo.tbProdutoComanda", "ProdutoComandaID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tbProdutoComanda", "ProdutoComandaID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tbProdutoComanda");
            DropColumn("dbo.tbProdutoComanda", "ProdutoComandaID");
            AddPrimaryKey("dbo.tbProdutoComanda", new[] { "ComandaID", "ProdutoID" });
        }
    }
}
