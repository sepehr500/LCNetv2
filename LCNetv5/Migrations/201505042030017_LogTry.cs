namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogTry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ChangeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ClientId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientChanges", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClientChanges", "ClientId", "dbo.Clients");
            DropIndex("dbo.ClientChanges", new[] { "UserId" });
            DropIndex("dbo.ClientChanges", new[] { "ClientId" });
            DropTable("dbo.ClientChanges");
        }
    }
}
