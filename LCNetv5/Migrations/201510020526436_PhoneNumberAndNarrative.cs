namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneNumberAndNarrative : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "PhoneNumber", c => c.Int());
            AddColumn("dbo.Clients", "Narrative", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Narrative");
            DropColumn("dbo.Clients", "PhoneNumber");
        }
    }
}
