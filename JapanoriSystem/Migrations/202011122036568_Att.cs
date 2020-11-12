namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Att : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbVendas", "Comanda", c => c.Int(nullable: false));
            AddColumn("dbo.tbVendas", "ValorTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbVendas", "ValorTotal");
            DropColumn("dbo.tbVendas", "Comanda");
        }
    }
}
