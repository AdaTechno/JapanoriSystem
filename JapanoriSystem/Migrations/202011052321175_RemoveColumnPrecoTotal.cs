namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnPrecoTotal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbComanda", "PrecoTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbComanda", "PrecoTotal", c => c.Double(nullable: false));
        }
    }
}
