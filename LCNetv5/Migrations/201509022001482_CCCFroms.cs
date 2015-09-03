namespace LCNetv5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CCCFroms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntryForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HowFindOut = c.Int(nullable: false),
                        NumberInHousehold = c.Int(nullable: false),
                        HomeOwner = c.Boolean(nullable: false),
                        PayRent = c.Boolean(nullable: false),
                        AmtRent = c.Int(),
                        AmountOfTimeInCommunity = c.Int(nullable: false),
                        WhereLiveBefore = c.String(),
                        WhyMove = c.String(),
                        TransportTypes = c.Int(nullable: false),
                        FinanceExperience = c.Boolean(nullable: false),
                        FirstLoanFor = c.Int(nullable: false),
                        Business = c.Boolean(nullable: false),
                        TypeOfWork = c.String(),
                        Misc = c.String(),
                        Client_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.StandardForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InterestGood = c.Boolean(nullable: false),
                        RepaySchedGood = c.Boolean(nullable: false),
                        PriceShock = c.Boolean(),
                        StepsTaken = c.String(),
                        AdjustModeOfIncome = c.Boolean(),
                        BuisnessImped = c.Boolean(),
                        ChangeBuisnessModel = c.Boolean(),
                        LessThan90Days = c.Boolean(),
                        FormalSavings = c.Boolean(nullable: false),
                        InformalSavings = c.Boolean(nullable: false),
                        CapitalInvest = c.Boolean(nullable: false),
                        Freedom = c.Boolean(nullable: false),
                        Seasonal = c.Boolean(),
                        HowSoSeasonal = c.String(),
                        WhatLargerLoanFor = c.String(),
                        FamilyDoingWell = c.Boolean(),
                        ProblemsToChange = c.String(),
                        HowCanWeHelp = c.String(),
                        Business = c.Boolean(nullable: false),
                        TypeOfWork = c.String(),
                        Misc = c.String(),
                        Client_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StandardForms", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StandardForms", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.EntryForms", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EntryForms", "Client_Id", "dbo.Clients");
            DropIndex("dbo.StandardForms", new[] { "User_Id" });
            DropIndex("dbo.StandardForms", new[] { "Client_Id" });
            DropIndex("dbo.EntryForms", new[] { "User_Id" });
            DropIndex("dbo.EntryForms", new[] { "Client_Id" });
            DropTable("dbo.StandardForms");
            DropTable("dbo.EntryForms");
        }
    }
}
