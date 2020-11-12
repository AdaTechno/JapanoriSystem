namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbProdutoComandaVendas", "VendaID", "dbo.tbVendas");
            DropPrimaryKey("dbo.tbVendas");
            AlterColumn("dbo.tbVendas", "VendaID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tbVendas", "VendaID");
            AddForeignKey("dbo.tbProdutoComandaVendas", "VendaID", "dbo.tbVendas", "VendaID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbProdutoComandaVendas", "VendaID", "dbo.tbVendas");
            DropPrimaryKey("dbo.tbVendas");
            AlterColumn("dbo.tbVendas", "VendaID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tbVendas", "VendaID");
            AddForeignKey("dbo.tbProdutoComandaVendas", "VendaID", "dbo.tbVendas", "VendaID", cascadeDelete: true);
        }
    }
}
