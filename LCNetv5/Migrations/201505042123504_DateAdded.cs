namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientChanges", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.LoanChanges", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentChanges", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentChanges", "Date");
            DropColumn("dbo.LoanChanges", "Date");
            DropColumn("dbo.ClientChanges", "Date");
        }
    }
}
