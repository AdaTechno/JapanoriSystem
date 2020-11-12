namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComandaValorTotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbComanda", "ValorTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbComanda", "ValorTotal");
        }
    }
}
