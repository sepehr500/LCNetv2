namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientChanges", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.LoanChanges", "LoanId", "dbo.Loans");
            DropForeignKey("dbo.PaymentChanges", "PaymentId", "dbo.Payments");
            DropIndex("dbo.ClientChanges", new[] { "ClientId" });
            DropIndex("dbo.LoanChanges", new[] { "LoanId" });
            DropIndex("dbo.PaymentChanges", new[] { "PaymentId" });
            AddColumn("dbo.ClientChanges", "Info", c => c.String());
            AddColumn("dbo.LoanChanges", "Info", c => c.String());
            AddColumn("dbo.PaymentChanges", "Info", c => c.String());
            DropColumn("dbo.ClientChanges", "ClientId");
            DropColumn("dbo.ClientChanges", "DeleteInfo");
            DropColumn("dbo.LoanChanges", "LoanId");
            DropColumn("dbo.LoanChanges", "DeleteInfo");
            DropColumn("dbo.PaymentChanges", "PaymentId");
            DropColumn("dbo.PaymentChanges", "DeleteInfo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentChanges", "DeleteInfo", c => c.String());
            AddColumn("dbo.PaymentChanges", "PaymentId", c => c.Int(nullable: false));
            AddColumn("dbo.LoanChanges", "DeleteInfo", c => c.String());
            AddColumn("dbo.LoanChanges", "LoanId", c => c.Int(nullable: false));
            AddColumn("dbo.ClientChanges", "DeleteInfo", c => c.String());
            AddColumn("dbo.ClientChanges", "ClientId", c => c.Int());
            DropColumn("dbo.PaymentChanges", "Info");
            DropColumn("dbo.LoanChanges", "Info");
            DropColumn("dbo.ClientChanges", "Info");
            CreateIndex("dbo.PaymentChanges", "PaymentId");
            CreateIndex("dbo.LoanChanges", "LoanId");
            CreateIndex("dbo.ClientChanges", "ClientId");
            AddForeignKey("dbo.PaymentChanges", "PaymentId", "dbo.Payments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoanChanges", "LoanId", "dbo.Loans", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClientChanges", "ClientId", "dbo.Clients", "Id");
        }
    }
}
