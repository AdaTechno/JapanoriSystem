namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnValorTotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbProdutoComanda", "ValorTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbProdutoComanda", "ValorTotal");
        }
    }
}
