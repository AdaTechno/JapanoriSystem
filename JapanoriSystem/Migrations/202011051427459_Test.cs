namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbProdutoComanda", "Quantidade", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbProdutoComanda", "Quantidade");
        }
    }
}
