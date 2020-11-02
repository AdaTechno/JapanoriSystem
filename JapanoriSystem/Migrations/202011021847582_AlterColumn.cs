namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbComanda", "PrecoTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbComanda", "PrecoTotal");
        }
    }
}
