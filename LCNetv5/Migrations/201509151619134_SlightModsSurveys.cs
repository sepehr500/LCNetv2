namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SlightModsSurveys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StandardForms", "LoanId", "dbo.Loans");
            DropIndex("dbo.StandardForms", new[] { "LoanId" });
            AddColumn("dbo.StandardForms", "LoanAmt", c => c.Double());
            AlterColumn("dbo.StandardForms", "LoanId", c => c.Int());
            CreateIndex("dbo.StandardForms", "LoanId");
            AddForeignKey("dbo.StandardForms", "LoanId", "dbo.Loans", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StandardForms", "LoanId", "dbo.Loans");
            DropIndex("dbo.StandardForms", new[] { "LoanId" });
            AlterColumn("dbo.StandardForms", "LoanId", c => c.Int(nullable: false));
            DropColumn("dbo.StandardForms", "LoanAmt");
            CreateIndex("dbo.StandardForms", "LoanId");
            AddForeignKey("dbo.StandardForms", "LoanId", "dbo.Loans", "Id", cascadeDelete: true);
        }
    }
}
