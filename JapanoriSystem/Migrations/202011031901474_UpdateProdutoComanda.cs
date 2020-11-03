namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProdutoComanda : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbProdutoComanda", "comandaList_DataGroupField");
            DropColumn("dbo.tbProdutoComanda", "comandaList_DataTextField");
            DropColumn("dbo.tbProdutoComanda", "comandaList_DataValueField");
            DropColumn("dbo.tbProdutoComanda", "produtoList_DataGroupField");
            DropColumn("dbo.tbProdutoComanda", "produtoList_DataTextField");
            DropColumn("dbo.tbProdutoComanda", "produtoList_DataValueField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbProdutoComanda", "produtoList_DataValueField", c => c.String());
            AddColumn("dbo.tbProdutoComanda", "produtoList_DataTextField", c => c.String());
            AddColumn("dbo.tbProdutoComanda", "produtoList_DataGroupField", c => c.String());
            AddColumn("dbo.tbProdutoComanda", "comandaList_DataValueField", c => c.String());
            AddColumn("dbo.tbProdutoComanda", "comandaList_DataTextField", c => c.String());
            AddColumn("dbo.tbProdutoComanda", "comandaList_DataGroupField", c => c.String());
        }
    }
}
