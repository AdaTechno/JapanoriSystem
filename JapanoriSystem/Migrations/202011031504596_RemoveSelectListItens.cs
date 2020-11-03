namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSelectListItens : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbComanda", "comandalist_DataGroupField");
            DropColumn("dbo.tbComanda", "comandalist_DataTextField");
            DropColumn("dbo.tbComanda", "comandalist_DataValueField");
            DropColumn("dbo.tbProduto", "produtoList_DataGroupField");
            DropColumn("dbo.tbProduto", "produtoList_DataTextField");
            DropColumn("dbo.tbProduto", "produtoList_DataValueField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbProduto", "produtoList_DataValueField", c => c.String());
            AddColumn("dbo.tbProduto", "produtoList_DataTextField", c => c.String());
            AddColumn("dbo.tbProduto", "produtoList_DataGroupField", c => c.String());
            AddColumn("dbo.tbComanda", "comandalist_DataValueField", c => c.String());
            AddColumn("dbo.tbComanda", "comandalist_DataTextField", c => c.String());
            AddColumn("dbo.tbComanda", "comandalist_DataGroupField", c => c.String());
        }
    }
}
