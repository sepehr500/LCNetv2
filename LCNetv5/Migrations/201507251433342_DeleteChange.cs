namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientChanges", "ClientId", "dbo.Clients");
            DropIndex("dbo.ClientChanges", new[] { "ClientId" });
            AddColumn("dbo.ClientChanges", "DeleteInfo", c => c.String());
            AddColumn("dbo.LoanChanges", "DeleteInfo", c => c.String());
            AddColumn("dbo.PaymentChanges", "DeleteInfo", c => c.String());
            AlterColumn("dbo.ClientChanges", "ClientId", c => c.Int());
            CreateIndex("dbo.ClientChanges", "ClientId");
            AddForeignKey("dbo.ClientChanges", "ClientId", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientChanges", "ClientId", "dbo.Clients");
            DropIndex("dbo.ClientChanges", new[] { "ClientId" });
            AlterColumn("dbo.ClientChanges", "ClientId", c => c.Int(nullable: false));
            DropColumn("dbo.PaymentChanges", "DeleteInfo");
            DropColumn("dbo.LoanChanges", "DeleteInfo");
            DropColumn("dbo.ClientChanges", "DeleteInfo");
            CreateIndex("dbo.ClientChanges", "ClientId");
            AddForeignKey("dbo.ClientChanges", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}
