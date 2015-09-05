namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherSmallFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntryForms", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.StandardForms", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StandardForms", "EndTime");
            DropColumn("dbo.EntryForms", "EndTime");
        }
    }
}
