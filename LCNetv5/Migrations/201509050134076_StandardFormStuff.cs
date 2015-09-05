namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandardFormStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntryForms", "LoanFor", c => c.Int(nullable: false));
            AddColumn("dbo.StandardForms", "LoanFor", c => c.Int(nullable: false));
            DropColumn("dbo.EntryForms", "FirstLoanFor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EntryForms", "FirstLoanFor", c => c.Int(nullable: false));
            DropColumn("dbo.StandardForms", "LoanFor");
            DropColumn("dbo.EntryForms", "LoanFor");
        }
    }
}
