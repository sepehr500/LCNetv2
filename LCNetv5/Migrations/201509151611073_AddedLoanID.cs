namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StandardForms", "ClientId", "dbo.Clients");
            DropIndex("dbo.StandardForms", new[] { "ClientId" });
            RenameColumn(table: "dbo.StandardForms", name: "ClientId", newName: "Client_Id");
            AddColumn("dbo.StandardForms", "LoanId", c => c.Int(nullable: false));
            AlterColumn("dbo.StandardForms", "Client_Id", c => c.Int());
            CreateIndex("dbo.StandardForms", "LoanId");
            CreateIndex("dbo.StandardForms", "Client_Id");
            AddForeignKey("dbo.StandardForms", "LoanId", "dbo.Loans", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StandardForms", "Client_Id", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StandardForms", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.StandardForms", "LoanId", "dbo.Loans");
            DropIndex("dbo.StandardForms", new[] { "Client_Id" });
            DropIndex("dbo.StandardForms", new[] { "LoanId" });
            AlterColumn("dbo.StandardForms", "Client_Id", c => c.Int(nullable: false));
            DropColumn("dbo.StandardForms", "LoanId");
            RenameColumn(table: "dbo.StandardForms", name: "Client_Id", newName: "ClientId");
            CreateIndex("dbo.StandardForms", "ClientId");
            AddForeignKey("dbo.StandardForms", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}
