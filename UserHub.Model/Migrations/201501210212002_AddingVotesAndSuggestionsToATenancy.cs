namespace UserHub.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingVotesAndSuggestionsToATenancy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Suggestions", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "CreatedBy_Id", "dbo.AspNetUsers");
            AddColumn("dbo.Suggestions", "Tenancy_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Votes", "Tenancy_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Suggestions", "Tenancy_Id");
            CreateIndex("dbo.Votes", "Tenancy_Id");
            AddForeignKey("dbo.Votes", "Tenancy_Id", "dbo.Tenancies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Suggestions", "Tenancy_Id", "dbo.Tenancies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Suggestions", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Votes", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Suggestions", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Suggestions", "Tenancy_Id", "dbo.Tenancies");
            DropForeignKey("dbo.Votes", "Tenancy_Id", "dbo.Tenancies");
            DropIndex("dbo.Votes", new[] { "Tenancy_Id" });
            DropIndex("dbo.Suggestions", new[] { "Tenancy_Id" });
            DropColumn("dbo.Votes", "Tenancy_Id");
            DropColumn("dbo.Suggestions", "Tenancy_Id");
            AddForeignKey("dbo.Votes", "CreatedBy_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Suggestions", "CreatedBy_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
