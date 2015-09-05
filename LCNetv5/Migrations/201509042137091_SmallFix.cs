namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntryForms", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.EntryForms", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.EntryForms", "Savings", c => c.Boolean(nullable: false));
            AddColumn("dbo.StandardForms", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.StandardForms", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.StandardForms", "Savings", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StandardForms", "Savings");
            DropColumn("dbo.StandardForms", "StartTime");
            DropColumn("dbo.StandardForms", "Date");
            DropColumn("dbo.EntryForms", "Savings");
            DropColumn("dbo.EntryForms", "StartTime");
            DropColumn("dbo.EntryForms", "Date");
        }
    }
}
