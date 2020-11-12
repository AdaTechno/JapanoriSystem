namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbProduto", "cDesc", c => c.String());
            DropColumn("dbo.tbProduto", "Desc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbProduto", "Desc", c => c.String());
            DropColumn("dbo.tbProduto", "cDesc");
        }
    }
}
