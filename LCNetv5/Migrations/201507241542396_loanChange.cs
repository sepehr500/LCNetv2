namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loanChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "TimePeriod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "TimePeriod");
        }
    }
}
