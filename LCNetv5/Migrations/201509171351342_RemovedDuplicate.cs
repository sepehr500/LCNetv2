namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDuplicate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StandardForms", "FormalSavings");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StandardForms", "FormalSavings", c => c.Boolean(nullable: false));
        }
    }
}
