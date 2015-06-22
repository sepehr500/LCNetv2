namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WowAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoanId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ChangeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Loans", t => t.LoanId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.LoanId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PaymentChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ChangeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PaymentId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentChanges", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentChanges", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.LoanChanges", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LoanChanges", "LoanId", "dbo.Loans");
            DropIndex("dbo.PaymentChanges", new[] { "UserId" });
            DropIndex("dbo.PaymentChanges", new[] { "PaymentId" });
            DropIndex("dbo.LoanChanges", new[] { "UserId" });
            DropIndex("dbo.LoanChanges", new[] { "LoanId" });
            DropTable("dbo.PaymentChanges");
            DropTable("dbo.LoanChanges");
        }
    }
}
