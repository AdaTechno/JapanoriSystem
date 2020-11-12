namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbProdutoComanda", "Quantidade", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbProdutoComanda", "Quantidade", c => c.Int(nullable: false));
        }
    }
}
