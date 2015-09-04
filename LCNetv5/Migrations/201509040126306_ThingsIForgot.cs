namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThingsIForgot : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EntryForms", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.StandardForms", "Client_Id", "dbo.Clients");
            DropIndex("dbo.EntryForms", new[] { "Client_Id" });
            DropIndex("dbo.StandardForms", new[] { "Client_Id" });
            RenameColumn(table: "dbo.EntryForms", name: "Client_Id", newName: "ClientId");
            RenameColumn(table: "dbo.StandardForms", name: "Client_Id", newName: "ClientId");
            RenameColumn(table: "dbo.EntryForms", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.StandardForms", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.EntryForms", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.StandardForms", name: "IX_User_Id", newName: "IX_UserId");
            AlterColumn("dbo.EntryForms", "ClientId", c => c.Int(nullable: false));
            AlterColumn("dbo.StandardForms", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.EntryForms", "ClientId");
            CreateIndex("dbo.StandardForms", "ClientId");
            AddForeignKey("dbo.EntryForms", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StandardForms", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StandardForms", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.EntryForms", "ClientId", "dbo.Clients");
            DropIndex("dbo.StandardForms", new[] { "ClientId" });
            DropIndex("dbo.EntryForms", new[] { "ClientId" });
            AlterColumn("dbo.StandardForms", "ClientId", c => c.Int());
            AlterColumn("dbo.EntryForms", "ClientId", c => c.Int());
            RenameIndex(table: "dbo.StandardForms", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.EntryForms", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.StandardForms", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.EntryForms", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.StandardForms", name: "ClientId", newName: "Client_Id");
            RenameColumn(table: "dbo.EntryForms", name: "ClientId", newName: "Client_Id");
            CreateIndex("dbo.StandardForms", "Client_Id");
            CreateIndex("dbo.EntryForms", "Client_Id");
            AddForeignKey("dbo.StandardForms", "Client_Id", "dbo.Clients", "Id");
            AddForeignKey("dbo.EntryForms", "Client_Id", "dbo.Clients", "Id");
        }
    }
}
