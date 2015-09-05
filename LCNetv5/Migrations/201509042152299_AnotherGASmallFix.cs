namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherGASmallFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EntryForms", "Date");
            DropColumn("dbo.StandardForms", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StandardForms", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.EntryForms", "Date", c => c.DateTime(nullable: false));
        }
    }
}
