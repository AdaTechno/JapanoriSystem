namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigrationVendas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbVendas", "FormaPag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbVendas", "FormaPag");
        }
    }
}
