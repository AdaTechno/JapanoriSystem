namespace JapanoriSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbComanda", "cStatus", c => c.String());
            AddColumn("dbo.tbProdutoComanda", "cStatus", c => c.String());
            AddColumn("dbo.tbProduto", "cStatus", c => c.String());
            AddColumn("dbo.tbEstoque", "cStatus", c => c.String());
            AddColumn("dbo.tbFuncionario", "cStatus", c => c.String(nullable: false));
            DropColumn("dbo.tbComanda", "Status");
            DropColumn("dbo.tbProdutoComanda", "Status");
            DropColumn("dbo.tbProduto", "Status");
            DropColumn("dbo.tbEstoque", "Status");
            DropColumn("dbo.tbFuncionario", "Status");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbFuncionario", "Status", c => c.String(nullable: false));
            AddColumn("dbo.tbEstoque", "Status", c => c.String());
            AddColumn("dbo.tbProduto", "Status", c => c.String());
            AddColumn("dbo.tbProdutoComanda", "Status", c => c.String());
            AddColumn("dbo.tbComanda", "Status", c => c.String());
            DropColumn("dbo.tbFuncionario", "cStatus");
            DropColumn("dbo.tbEstoque", "cStatus");
            DropColumn("dbo.tbProduto", "cStatus");
            DropColumn("dbo.tbProdutoComanda", "cStatus");
            DropColumn("dbo.tbComanda", "cStatus");
        }
    }
}
